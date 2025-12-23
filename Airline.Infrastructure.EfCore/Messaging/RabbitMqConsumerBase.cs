using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Airline.Infrastructure.EfCore.Messaging;

/// <summary>
/// Базовый класс для консьюмеров RabbitMQ с политикой повторных попыток.
/// </summary>
public abstract class RabbitMqConsumerBase<TMessage>(
    ILogger logger,
    IServiceProvider serviceProvider,
    IConnection connection,
    string queueName) : BackgroundService where TMessage : class
{
    protected readonly ILogger Logger = logger;
    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    private IModel? _channel;
    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(
            retryCount: 10,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt), 60)),
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                logger.LogWarning(
                    exception,
                    "Ошибка подключения к RabbitMQ. Попытка {RetryCount} через {TimeSpan}.",
                    retryCount,
                    timeSpan);
            });

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Консьюмер {QueueName} запускается...", queueName);

        try
        {
            await _retryPolicy.ExecuteAsync(() =>
            {
                _channel = connection.CreateModel();

                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Logger.LogInformation("Консьюмер подключён к очереди {QueueName}.", queueName);
                return Task.CompletedTask;
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Критическая ошибка при запуске консьюмера {QueueName}.", queueName);
            return;
        }

        if (_channel == null)
        {
            Logger.LogError("Не удалось создать канал RabbitMQ.");
            return;
        }

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<TMessage>(json);

                if (message != null)
                {
                    await ProcessMessageAsync(message, stoppingToken);
                    _channel.BasicAck(ea.DeliveryTag, multiple: false);
                    Logger.LogDebug("Сообщение обработано в очереди {QueueName}.", queueName);
                }
                else
                {
                    Logger.LogWarning("Получено пустое сообщение в очереди {QueueName}.", queueName);
                    _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Ошибка обработки сообщения в очереди {QueueName}.", queueName);
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(
            queue: queueName,
            autoAck: false,
            consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    /// <summary>
    /// Обрабатывает полученное сообщение.
    /// </summary>
    protected abstract Task ProcessMessageAsync(TMessage message, CancellationToken cancellationToken);

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Консьюмер {QueueName} останавливается...", queueName);

        if (_channel != null)
        {
            _channel.Close();
            _channel.Dispose();
        }

        // IConnection управляется Aspire DI, не закрываем его здесь

        return base.StopAsync(cancellationToken);
    }
}
