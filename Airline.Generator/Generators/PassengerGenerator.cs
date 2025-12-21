using Bogus;
using Airline.Messaging.Contracts.Messages;

namespace Airline.Generator.Generators;

/// <summary>
/// Генератор данных пассажиров с использованием Bogus.
/// </summary>
public class PassengerGenerator
{
    private readonly Faker<CreatePassengerMessage> _faker;

    public PassengerGenerator()
    {
        Randomizer.Seed = new Random(42);

        _faker = new Faker<CreatePassengerMessage>("ru")
            .CustomInstantiator(f =>
            {
                var gender = f.PickRandom(Bogus.DataSets.Name.Gender.Male, Bogus.DataSets.Name.Gender.Female);
                var firstName = f.Name.FirstName(gender);
                var lastName = f.Name.LastName(gender);
                
                string? patronymic = null;
                if (f.Random.Bool(0.8f))
                {
                    var patronymicBase = f.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                    patronymic = gender == Bogus.DataSets.Name.Gender.Male 
                        ? patronymicBase + "ович" 
                        : patronymicBase + "овна";
                }
                
                return new CreatePassengerMessage
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Patronymic = patronymic,
                    PassportNumber = f.Random.ReplaceNumbers("#### ######"),
                    BirthDate = DateOnly.FromDateTime(f.Date.Past(60, DateTime.Now.AddYears(-18)))
                };
            });
    }

    /// <summary>
    /// Генерирует указанное количество пассажиров.
    /// </summary>
    public IEnumerable<CreatePassengerMessage> Generate(int count)
    {
        return _faker.Generate(count);
    }

    /// <summary>
    /// Генерирует одного пассажира.
    /// </summary>
    public CreatePassengerMessage Generate()
    {
        return _faker.Generate();
    }
}
