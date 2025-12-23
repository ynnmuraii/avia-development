using Airline.Generator.Generators;
using Airline.Generator.Services;
using Airline.Messaging.Contracts;

namespace Airline.Generator.Workers;

/// <summary>
/// Фоновая служба для генерации и публикации билетов в RabbitMQ.
/// </summary>
public class DataGeneratorWorker(
    ILogger<DataGeneratorWorker> logger,
    RabbitMqPublisher publisher,
    TicketGenerator ticketGenerator) : BackgroundService
{
    private const int TicketsToGenerate = 100;
    private const int GenerationIntervalMs = 30000;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("DataGeneratorWorker запущен.");

        await Task.Delay(5000, stoppingToken);

        var cycleCount = 0;
        const int maxCycles = 1;

        while (!stoppingToken.IsCancellationRequested && cycleCount < maxCycles)
        {
            try
            {
                cycleCount++;
                logger.LogInformation("Начало цикла генерации {Cycle} из {Max}", cycleCount, maxCycles);
                await GenerateTicketsAsync(stoppingToken);
                logger.LogInformation("Цикл генерации данных {Cycle} завершён.", cycleCount);

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
                logger.LogError(ex, "Ошибка при генерации данных.");
                await Task.Delay(10000, stoppingToken);
            }
        }

        logger.LogInformation("DataGeneratorWorker остановлен после {Cycles} циклов.", cycleCount);
    }

    private async Task GenerateTicketsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Начало генерации билетов...");

        for (var i = 0; i < TicketsToGenerate; i++)
        {
            var ticket = ticketGenerator.Generate();
            await publisher.PublishAsync(QueueNames.Tickets, ticket, cancellationToken);
        }

        logger.LogInformation("Сгенерировано {Count} билетов.", TicketsToGenerate);
        logger.LogInformation("Генерация билетов завершена.");
    }
}
