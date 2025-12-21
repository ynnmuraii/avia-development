using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных рейсов с использованием Bogus.
/// </summary>
public class FlightGenerator
{
    private readonly Faker _faker;
    private static readonly string[] AirportCodes = [
        "SVO", "DME", "VKO", "LED", "KZN", "SVX", "OVB", "KRR", "AER", "ROV",
        "UFA", "VVO", "KHV", "IKT", "TOF", "CEK", "SGC", "MCX", "GOJ", "NJC",
        "JFK", "LAX", "ORD", "DFW", "DEN", "ATL", "MIA", "SFO", "SEA", "BOS"
    ];

    public FlightGenerator()
    {
        _faker = new Faker();
    }

    /// <summary>
    /// Генерирует указанное количество рейсов с указанным ModelId.
    /// </summary>
    public IEnumerable<CreateFlightMessage> Generate(int count, int modelId)
    {
        return Enumerable.Range(0, count).Select(_ => GenerateWithModelId(modelId));
    }

    /// <summary>
    /// Генерирует один рейс с указанным ModelId.
    /// </summary>
    public CreateFlightMessage GenerateWithModelId(int modelId)
    {
        var from = _faker.PickRandom(AirportCodes);
        var to = _faker.PickRandom(AirportCodes.Where(c => c != from).ToArray());
        
        var departureDate = DateOnly.FromDateTime(_faker.Date.Future(180));
        var departureTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(_faker.Random.Int(0, 23)) + TimeSpan.FromMinutes(_faker.Random.Int(0, 59)));
        var duration = TimeSpan.FromMinutes(_faker.Random.Int(60, 720));
        
        var arrivalDate = departureDate;
        if (departureTime.Add(duration) < departureTime)
        {
            arrivalDate = departureDate.AddDays(1);
        }

        var code = _faker.Random.ReplaceNumbers("??####").Replace('?', _faker.PickRandom("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()));
        code = new string(code.Take(2).ToArray()) + _faker.Random.Int(100, 9999).ToString();

        return new CreateFlightMessage(
            Code: code,
            From: from,
            To: to,
            DateOfDeparture: departureDate,
            DateOfArrival: arrivalDate,
            TimeOfDeparture: departureTime,
            FlightDuration: duration,
            ModelId: modelId
        );
    }

    /// <summary>
    /// Генерирует один рейс с случайным ModelId.
    /// </summary>
    public CreateFlightMessage Generate()
    {
        return GenerateWithModelId(_faker.Random.Int(1, 10));
    }
}
