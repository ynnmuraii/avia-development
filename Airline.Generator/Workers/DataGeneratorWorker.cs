using Airline.Generator.Generators;
using Airline.Generator.Services;
using Airline.Messaging.Contracts;

namespace Airline.Generator.Workers;

/// <summary>
/// Фоновая служба для генерации и публикации билетов в RabbitMQ.
/// </summary>
public class DataGeneratorWorker : BackgroundService
{
    private readonly ILogger<DataGeneratorWorker> _logger;
    private readonly RabbitMqPublisher _publisher;
    private readonly TicketGenerator _ticketGenerator;

    private const int TicketsToGenerate = 100;
    private const int GenerationIntervalMs = 30000;

    public DataGeneratorWorker(
        ILogger<DataGeneratorWorker> logger,
        RabbitMqPublisher publisher,
        TicketGenerator ticketGenerator)
    {
        _logger = logger;
        _publisher = publisher;
        _ticketGenerator = ticketGenerator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DataGeneratorWorker запущен.");

        await Task.Delay(5000, stoppingToken);

        var cycleCount = 0;
        const int maxCycles = 1;

        while (!stoppingToken.IsCancellationRequested && cycleCount < maxCycles)
        {
            try
            {
                cycleCount++;
                _logger.LogInformation("Начало цикла генерации {Cycle} из {Max}", cycleCount, maxCycles);
                await GenerateTicketsAsync(stoppingToken);
                _logger.LogInformation("Цикл генерации данных {Cycle} завершён.", cycleCount);

                if (cycleCount < maxCycles)
                {
                    await Task.Delay(GenerationIntervalMs, stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при генерации данных.");
                await Task.Delay(10000, stoppingToken);
            }
        }

        _logger.LogInformation("DataGeneratorWorker остановлен после {Cycles} циклов.", cycleCount);
    }

    private async Task GenerateTicketsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало генерации билетов...");

        for (var i = 0; i < TicketsToGenerate; i++)
        {
            var ticket = _ticketGenerator.Generate();
            await _publisher.PublishAsync(QueueNames.Tickets, ticket, cancellationToken);
        }

        _logger.LogInformation("Сгенерировано {Count} билетов.", TicketsToGenerate);
        _logger.LogInformation("Генерация билетов завершена.");
    }
}
