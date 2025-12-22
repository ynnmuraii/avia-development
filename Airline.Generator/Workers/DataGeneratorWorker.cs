using Airline.Generator.Generators;
using Airline.Generator.Services;
using Airline.Messaging.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Airline.Generator.Workers;

/// <summary>
/// Фоновая служба для генерации и публикации данных в RabbitMQ.
/// </summary>
public class DataGeneratorWorker : BackgroundService
{
    private readonly ILogger<DataGeneratorWorker> _logger;
    private readonly RabbitMqPublisher _publisher;
    private readonly PassengerGenerator _passengerGenerator;
    private readonly AircraftFamilyGenerator _aircraftFamilyGenerator;
    private readonly AircraftModelGenerator _aircraftModelGenerator;
    private readonly FlightGenerator _flightGenerator;
    private readonly TicketGenerator _ticketGenerator;

    private const int FamiliesToGenerate = 100;
    private const int ModelsPerFamily = 5;
    private const int FlightsPerModel = 5;
    private const int PassengersToGenerate = 100;
    private const int TicketsPerFlight = 5;
    private const int GenerationIntervalMs = 30000; 

    public DataGeneratorWorker(
        ILogger<DataGeneratorWorker> logger,
        RabbitMqPublisher publisher,
        PassengerGenerator passengerGenerator,
        AircraftFamilyGenerator aircraftFamilyGenerator,
        AircraftModelGenerator aircraftModelGenerator,
        FlightGenerator flightGenerator,
        TicketGenerator ticketGenerator)
    {
        _logger = logger;
        _publisher = publisher;
        _passengerGenerator = passengerGenerator;
        _aircraftFamilyGenerator = aircraftFamilyGenerator;
        _aircraftModelGenerator = aircraftModelGenerator;
        _flightGenerator = flightGenerator;
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
                await GenerateDataAsync(stoppingToken);
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

    private async Task GenerateDataAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало генерации данных...");

        var families = _aircraftFamilyGenerator.Generate(FamiliesToGenerate).ToList();
        foreach (var family in families)
        {
            await _publisher.PublishAsync(QueueNames.AircraftFamilies, family, cancellationToken);
        }
        _logger.LogInformation("Сгенерировано {Count} семейств самолётов.", families.Count);

        await Task.Delay(1000, cancellationToken);

        var totalModels = 0;
        const int maxModels = 100;
        for (var i = 0; i < families.Count && totalModels < maxModels; i++)
        {
            var familyId = i + 1;
            var models = _aircraftModelGenerator.Generate(ModelsPerFamily, familyId).ToList();
            foreach (var model in models)
            {
                if (totalModels >= maxModels) break;
                await _publisher.PublishAsync(QueueNames.AircraftModels, model, cancellationToken);
                totalModels++;
            }
        }
        _logger.LogInformation("Сгенерировано {Count} моделей самолётов.", totalModels);

        await Task.Delay(1000, cancellationToken);

        var passengers = _passengerGenerator.Generate(PassengersToGenerate).ToList();
        foreach (var passenger in passengers)
        {
            await _publisher.PublishAsync(QueueNames.Passengers, passenger, cancellationToken);
        }
        _logger.LogInformation("Сгенерировано {Count} пассажиров.", passengers.Count);

        await Task.Delay(1000, cancellationToken);

        var totalFlights = 0;
        const int maxFlights = 100;
        for (var mid = 1; mid <= totalModels && totalFlights < maxFlights; mid++)
        {
            var flights = _flightGenerator.Generate(FlightsPerModel, mid).ToList();
            foreach (var flight in flights)
            {
                if (totalFlights >= maxFlights) break;
                await _publisher.PublishAsync(QueueNames.Flights, flight, cancellationToken);
                totalFlights++;
            }
        }
        _logger.LogInformation("Сгенерировано {Count} рейсов.", totalFlights);

        await Task.Delay(1000, cancellationToken);

        var totalTickets = 0;
        const int maxTickets = 100;
        var passengerIds = Enumerable.Range(1, passengers.Count).ToList();
        for (var fid = 1; fid <= totalFlights && totalTickets < maxTickets; fid++)
        {
            var tickets = _ticketGenerator.Generate(TicketsPerFlight, fid, passengerIds).ToList();
            foreach (var ticket in tickets)
            {
                if (totalTickets >= maxTickets) break;
                await _publisher.PublishAsync(QueueNames.Tickets, ticket, cancellationToken);
                totalTickets++;
            }
        }
        _logger.LogInformation("Сгенерировано {Count} билетов.", totalTickets);

        _logger.LogInformation("Генерация данных завершена.");
    }
}
