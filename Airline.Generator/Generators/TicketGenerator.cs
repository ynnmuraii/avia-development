using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных билетов с использованием Bogus.
/// </summary>
public class TicketGenerator
{
    private static readonly string[] _seatLetters = ["A", "B", "C", "D", "E", "F"];
    private readonly Faker _faker;

    public TicketGenerator()
    {
        _faker = new Faker();
    }

    /// <summary>
    /// Создаёт Faker для генерации билетов с указанными FlightId и PassengerId.
    /// </summary>
    private Faker<CreateTicketMessage> CreateTicketFaker(int flightId, int passengerId) =>
        new Faker<CreateTicketMessage>()
            .CustomInstantiator(f =>
            {
                var row = f.Random.Int(1, 45);
                var seat = f.PickRandom(_seatLetters);
                return new CreateTicketMessage(
                    FlightId: flightId,
                    PassengerId: passengerId,
                    SeatId: $"{row}{seat}",
                    HasCarryOn: f.Random.Bool(0.7f),
                    BaggageKg: f.Random.Bool(0.2f) ? 0 : Math.Round(f.Random.Double(0.5, 32), 3)
                );
            });

    /// <summary>
    /// Генерирует указанное количество билетов с указанными FlightId и PassengerId.
    /// </summary>
    public IEnumerable<CreateTicketMessage> Generate(int count, int flightId, IEnumerable<int> passengerIds)
    {
        var passengers = passengerIds.ToList();
        return Enumerable.Range(0, count).Select(i => GenerateWithIds(flightId, passengers[i % passengers.Count]));
    }

    /// <summary>
    /// Генерирует один билет с указанными FlightId и PassengerId.
    /// </summary>
    public CreateTicketMessage GenerateWithIds(int flightId, int passengerId) =>
        CreateTicketFaker(flightId, passengerId).Generate();

    /// <summary>
    /// Генерирует один билет со случайными ID.
    /// </summary>
    public CreateTicketMessage Generate() =>
        GenerateWithIds(_faker.Random.Int(1, 10), _faker.Random.Int(1, 15));
}
