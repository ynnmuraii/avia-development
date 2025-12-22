using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных билетов с использованием Bogus.
/// </summary>
public class TicketGenerator
{
    private readonly Faker _faker;
    private static readonly string[] _seatLetters = ["A", "B", "C", "D", "E", "F"];

    public TicketGenerator()
    {
        _faker = new Faker();
    }

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
    public CreateTicketMessage GenerateWithIds(int flightId, int passengerId)
    {
        var row = _faker.Random.Int(1, 45);
        var seat = _faker.PickRandom(_seatLetters);
        var seatId = $"{row}{seat}";

        return new CreateTicketMessage(
            FlightId: flightId,
            PassengerId: passengerId,
            SeatId: seatId,
            HasCarryOn: _faker.Random.Bool(0.7f),
            BaggageKg: _faker.Random.Bool(0.2f) ? 0 : Math.Round(_faker.Random.Double(0.5, 32), 3)
        );
    }

    /// <summary>
    /// Генерирует один билет со случайными ID.
    /// </summary>
    public CreateTicketMessage Generate()
    {
        return GenerateWithIds(_faker.Random.Int(1, 10), _faker.Random.Int(1, 15)); // от 1 до 15 для проверки валидации на несуществующие
    }
}
