using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных моделей самолётов с использованием Bogus.
/// </summary>
public class AircraftModelGenerator
{
    private readonly Faker<CreateAircraftModelMessage> _faker;
    private int _familyIdCounter = 1;

    public AircraftModelGenerator()
    {
        Randomizer.Seed = new Random(42);

        _faker = new Faker<CreateAircraftModelMessage>()
            .CustomInstantiator(f => new CreateAircraftModelMessage(
                ModelName: GenerateModelName(f),
                RangeKm: f.Random.Int(2000, 15000),
                Seats: f.Random.Int(100, 500),
                CargoCapacityKg: f.Random.Int(10000, 50000),
                FamilyId: 1
            ));
    }

    private static string GenerateModelName(Faker f)
    {
        var suffix = f.PickRandom(new[] { "MAX", "Neo", "ER", "LR", "SR", "" });
        var version = f.Random.Int(100, 900);
        return string.IsNullOrEmpty(suffix) ? $"{version}" : $"{version} {suffix}";
    }

    /// <summary>
    /// Генерирует указанное количество моделей самолётов с указанным FamilyId.
    /// </summary>
    public IEnumerable<CreateAircraftModelMessage> Generate(int count, int familyId)
    {
        return Enumerable.Range(0, count).Select(_ => GenerateWithFamilyId(familyId));
    }

    /// <summary>
    /// Генерирует одну модель самолёта с указанным FamilyId.
    /// </summary>
    public CreateAircraftModelMessage GenerateWithFamilyId(int familyId)
    {
        var template = _faker.Generate();
        return template with { FamilyId = familyId };
    }

    /// <summary>
    /// Генерирует одну модель с автоинкрементацией FamilyId.
    /// </summary>
    public CreateAircraftModelMessage Generate()
    {
        return GenerateWithFamilyId(_familyIdCounter++);
    }
}
