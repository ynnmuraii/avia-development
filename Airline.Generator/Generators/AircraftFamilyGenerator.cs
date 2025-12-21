using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных семейств самолётов с использованием Bogus.
/// </summary>
public class AircraftFamilyGenerator
{
    private readonly Faker _faker;
    private static readonly string[] Manufacturers = ["Boeing", "Airbus", "Embraer", "Bombardier", "Sukhoi", "Tupolev"];
    private static readonly Dictionary<string, string[]> FamiliesByManufacturer = new()
    {
        ["Boeing"] = ["737", "747", "757", "767", "777", "787 Dreamliner"],
        ["Airbus"] = ["A320", "A330", "A340", "A350", "A380"],
        ["Embraer"] = ["E170", "E175", "E190", "E195"],
        ["Bombardier"] = ["CRJ700", "CRJ900", "Q400"],
        ["Sukhoi"] = ["Superjet 100"],
        ["Tupolev"] = ["Tu-154", "Tu-204", "Tu-214"]
    };

    private int _currentIndex;

    public AircraftFamilyGenerator()
    {
        _faker = new Faker();
        _currentIndex = 0;
    }

    /// <summary>
    /// Генерирует указанное количество семейств самолётов.
    /// </summary>
    public IEnumerable<CreateAircraftFamilyMessage> Generate(int count)
    {
        var allFamilies = new List<CreateAircraftFamilyMessage>();
        
        foreach (var manufacturer in Manufacturers)
        {
            foreach (var family in FamiliesByManufacturer[manufacturer])
            {
                allFamilies.Add(new CreateAircraftFamilyMessage(manufacturer, family));
            }
        }

        return allFamilies.Take(count);
    }

    /// <summary>
    /// Генерирует одно семейство самолётов.
    /// </summary>
    public CreateAircraftFamilyMessage Generate()
    {
        var manufacturer = _faker.PickRandom(Manufacturers);
        var family = _faker.PickRandom(FamiliesByManufacturer[manufacturer]);
        return new CreateAircraftFamilyMessage(manufacturer, family);
    }
}
