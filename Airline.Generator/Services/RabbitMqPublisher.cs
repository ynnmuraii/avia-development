using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;

namespace Airline.Generator.Services;

/// <summary>
/// Сервис для публикации сообщений в RabbitMQ с политикой повторных попыток.
/// </summary>
public class RabbitMqPublisher : IAsyncDisposable
{
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly string _connectionString;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public RabbitMqPublisher(ILogger<RabbitMqPublisher> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("messaging") 
            ?? "amqp://guest:guest@localhost:5672";

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "Ошибка подключения к RabbitMQ. Попытка {RetryCount} через {TimeSpan}.",
                        retryCount,
                        timeSpan);
                });
    }

    /// <summary>
    /// Подключается к RabbitMQ с повторными попытками.
    /// </summary>
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        await _connectionLock.WaitAsync(cancellationToken);
        try
        {
            if (_connection?.IsOpen == true && _channel?.IsOpen == true)
                return;

            await _retryPolicy.ExecuteAsync(async () =>
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_connectionString)
                };

                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

                _logger.LogInformation("Успешное подключение к RabbitMQ.");
            });
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    /// <summary>
    /// Публикует сообщение в указанную очередь.
    /// </summary>
    public async Task PublishAsync<T>(string queueName, T message, CancellationToken cancellationToken = default)
    {
        await ConnectAsync(cancellationToken);

        if (_channel == null)
            throw new InvalidOperationException("Канал RabbitMQ не инициализирован.");

        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);

        _logger.LogDebug("Сообщение опубликовано в очередь {QueueName}.", queueName);
    }

    /// <summary>
    /// Публикует несколько сообщений в указанную очередь.
    /// </summary>
    public async Task PublishBatchAsync<T>(string queueName, IEnumerable<T> messages, CancellationToken cancellationToken = default)
    {
        foreach (var message in messages)
        {
            await PublishAsync(queueName, message, cancellationToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            _channel.Dispose();
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection.Dispose();
        }

        _connectionLock.Dispose();
    }
}
