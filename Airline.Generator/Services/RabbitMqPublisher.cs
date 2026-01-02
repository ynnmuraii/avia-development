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
public class RabbitMqPublisher(
    ILogger<RabbitMqPublisher> logger,
    IConnection connection) : IDisposable
{
    private IModel? _channel;
    private readonly object _connectionLock = new();
    private readonly RetryPolicy _retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                logger.LogWarning(
                    exception,
                    "Ошибка подключения к RabbitMQ. Попытка {RetryCount} через {TimeSpan}.",
                    retryCount,
                    timeSpan);
            });

    /// <summary>
    /// Подключается к RabbitMQ с повторными попытками.
    /// </summary>
    public void Connect()
    {
        lock (_connectionLock)
        {
            if (_channel?.IsOpen == true)
                return;

            _retryPolicy.Execute(() =>
            {
                _channel = connection.CreateModel();
                logger.LogInformation("Успешное подключение к RabbitMQ.");
            });
        }
    }

    /// <summary>
    /// Публикует сообщение в указанную очередь.
    /// </summary>
    public Task PublishAsync<T>(string queueName, T message, CancellationToken cancellationToken = default)
    {
        Connect();

        if (_channel == null)
            throw new InvalidOperationException("Канал RabbitMQ не инициализирован.");

        _channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";

        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            basicProperties: properties,
            body: body);

        logger.LogDebug("Сообщение опубликовано в очередь {QueueName}.", queueName);
        return Task.CompletedTask;
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

    public void Dispose()
    {
        if (_channel != null)
        {
            _channel.Close();
            _channel.Dispose();
        }
    }
}
