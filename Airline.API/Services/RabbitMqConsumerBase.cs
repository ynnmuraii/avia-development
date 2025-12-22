using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Airline.API.Services;

/// <summary>
/// Базовый класс для консьюмеров RabbitMQ с политикой повторных попыток.
/// </summary>
public abstract class RabbitMqConsumerBase<TMessage> : BackgroundService where TMessage : class
{
    protected readonly ILogger Logger;
    protected readonly IServiceProvider ServiceProvider;
    private readonly string _connectionString;
    private readonly string _queueName;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly AsyncRetryPolicy _retryPolicy;

    protected RabbitMqConsumerBase(
        ILogger logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        string queueName)
    {
        Logger = logger;
        ServiceProvider = serviceProvider;
        _queueName = queueName;
        _connectionString = configuration.GetConnectionString("messaging") 
            ?? "amqp://guest:guest@localhost:5672";

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 10,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt), 60)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    Logger.LogWarning(
                        exception,
                        "Ошибка подключения к RabbitMQ. Попытка {RetryCount} через {TimeSpan}.",
                        retryCount,
                        timeSpan);
                });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Консьюмер {QueueName} запускается...", _queueName);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

            Logger.LogInformation("Консьюмер подключён к очереди {QueueName}.", _queueName);
        });

        if (_channel == null)
        {
            Logger.LogError("Не удалось создать канал RabbitMQ.");
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<TMessage>(json);

                if (message != null)
                {
                    await ProcessMessageAsync(message, stoppingToken);
                    await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                    Logger.LogDebug("Сообщение обработано в очереди {QueueName}.", _queueName);
                }
                else
                {
                    Logger.LogWarning("Получено пустое сообщение в очереди {QueueName}.", _queueName);
                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Ошибка обработки сообщения в очереди {QueueName}.", _queueName);
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: _queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);


        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    /// <summary>
    /// Обрабатывает полученное сообщение.
    /// </summary>
    protected abstract Task ProcessMessageAsync(TMessage message, CancellationToken cancellationToken);

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Консьюмер {QueueName} останавливается...", _queueName);

        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken);
            _channel.Dispose();
        }

        if (_connection != null)
        {
            await _connection.CloseAsync(cancellationToken);
            _connection.Dispose();
        }

        await base.StopAsync(cancellationToken);
    }
}
