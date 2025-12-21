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

    // Конфигурация генерации
    private const int FamiliesToGenerate = 5;
    private const int ModelsPerFamily = 2;
    private const int FlightsPerModel = 3;
    private const int PassengersToGenerate = 50;
    private const int TicketsPerFlight = 10;
    private const int GenerationIntervalMs = 30000; // 30 секунд между циклами

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

        // Ждём готовности RabbitMQ
        await Task.Delay(5000, stoppingToken);

        var cycleCount = 0;
        const int maxCycles = 3;

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
                await Task.Delay(10000, stoppingToken); // Подождать перед повторной попыткой
            }
        }

        _logger.LogInformation("DataGeneratorWorker остановлен после {Cycles} циклов.", cycleCount);
    }

    private async Task GenerateDataAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало генерации данных...");

        // 1. Генерация семейств самолётов
        var families = _aircraftFamilyGenerator.Generate(FamiliesToGenerate).ToList();
        foreach (var family in families)
        {
            await _publisher.PublishAsync(QueueNames.AircraftFamilies, family, cancellationToken);
        }
        _logger.LogInformation("Сгенерировано {Count} семейств самолётов.", families.Count);

        // Небольшая задержка между типами сущностей
        await Task.Delay(1000, cancellationToken);

        // 2. Генерация моделей самолётов
        var modelId = 1;
        for (var familyId = 1; familyId <= families.Count; familyId++)
        {
            var models = _aircraftModelGenerator.Generate(ModelsPerFamily, familyId).ToList();
            foreach (var model in models)
            {
                await _publisher.PublishAsync(QueueNames.AircraftModels, model, cancellationToken);
                modelId++;
            }
        }
        _logger.LogInformation("Сгенерировано {Count} моделей самолётов.", families.Count * ModelsPerFamily);

        await Task.Delay(1000, cancellationToken);

        // 3. Генерация пассажиров
        var passengers = _passengerGenerator.Generate(PassengersToGenerate).ToList();
        foreach (var passenger in passengers)
        {
            await _publisher.PublishAsync(QueueNames.Passengers, passenger, cancellationToken);
        }
        _logger.LogInformation("Сгенерировано {Count} пассажиров.", passengers.Count);

        await Task.Delay(1000, cancellationToken);

        // 4. Генерация рейсов
        var flightId = 1;
        var totalModels = families.Count * ModelsPerFamily;
        for (var mid = 1; mid <= totalModels; mid++)
        {
            var flights = _flightGenerator.Generate(FlightsPerModel, mid).ToList();
            foreach (var flight in flights)
            {
                await _publisher.PublishAsync(QueueNames.Flights, flight, cancellationToken);
                flightId++;
            }
        }
        _logger.LogInformation("Сгенерировано {Count} рейсов.", totalModels * FlightsPerModel);

        await Task.Delay(1000, cancellationToken);

        // 5. Генерация билетов
        var totalFlights = totalModels * FlightsPerModel;
        var passengerIds = Enumerable.Range(1, passengers.Count).ToList();
        for (var fid = 1; fid <= totalFlights; fid++)
        {
            var tickets = _ticketGenerator.Generate(TicketsPerFlight, fid, passengerIds).ToList();
            foreach (var ticket in tickets)
            {
                await _publisher.PublishAsync(QueueNames.Tickets, ticket, cancellationToken);
            }
        }
        _logger.LogInformation("Сгенерировано {Count} билетов.", totalFlights * TicketsPerFlight);

        _logger.LogInformation("Генерация данных завершена.");
    }
}
