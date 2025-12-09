using Airline.Domain;

namespace Airline.Infrastructure.InMemory.Data;

/// <summary>
/// Сидер для инициализации тестовых данных авиакомпании в памяти.
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Получить начальные семейства самолётов.
    /// </summary>
    public static List<AircraftFamily> GetAircraftFamilies() => new()
    {
        new()
        {
            Id = 1,
            Manufacturer = "Airbus",
            FamilyName = "A320"
        },
        new()
        {
            Id = 2,
            Manufacturer = "Boeing",
            FamilyName = "737"
        },
        new()
        {
            Id = 3,
            Manufacturer = "Sukhoi",
            FamilyName = "Superjet 100"
        },
        new()
        {
            Id = 4,
            Manufacturer = "Irkut",
            FamilyName = "MC-21"
        },
        new()
        {
            Id = 5,
            Manufacturer = "Embraer",
            FamilyName = "E-Jet E2"
        },
        new()
        {
            Id = 6,
            Manufacturer = "Bombardier",
            FamilyName = "CRJ"
        },
        new()
        {
            Id = 7,
            Manufacturer = "Comac",
            FamilyName = "C919"
        },
        new()
        {
            Id = 8,
            Manufacturer = "Tupolev",
            FamilyName = "Tu-204"
        },
        new()
        {
            Id = 9,
            Manufacturer = "Ilyushin",
            FamilyName = "Il-114"
        },
        new()
        {
            Id = 10,
            Manufacturer = "De Havilland Canada",
            FamilyName = "Dash 8"
        }
    };

    /// <summary>
    /// Получить начальные модели самолётов.
    /// </summary>
    public static List<AircraftModel> GetAircraftModels(List<AircraftFamily> families) => new()
    {
        new()
        {
            Id = 1,
            ModelName = "A320-200",
            RangeKm = 6300,
            Seats = 194,
            CargoCapacityKg = 24000,
            Family = families.First(f => f.Id == 1)
        },
        new()
        {
            Id = 2,
            ModelName = "A320-NEO",
            RangeKm = 6850,
            Seats = 194,
            CargoCapacityKg = 26730,
            Family = families.First(f => f.Id == 1)
        },
        new()
        {
            Id = 3,
            ModelName = "737-800",
            RangeKm = 5436,
            Seats = 189,
            CargoCapacityKg = 25360,
            Family = families.First(f => f.Id == 2)
        },
        new()
        {
            Id = 4,
            ModelName = "737 MAX 8",
            RangeKm = 6570,
            Seats = 210,
            CargoCapacityKg = 27000,
            Family = families.First(f => f.Id == 2)
        },
        new()
        {
            Id = 5,
            ModelName = "SSJ 100",
            RangeKm = 4800,
            Seats = 108,
            CargoCapacityKg = 18000,
            Family = families.First(f => f.Id == 3)
        },
        new()
        {
            Id = 6,
            ModelName = "MC-21-310",
            RangeKm = 5600,
            Seats = 163,
            CargoCapacityKg = 20000,
            Family = families.First(f => f.Id == 4)
        },
        new()
        {
            Id = 7,
            ModelName = "E195-E2",
            RangeKm = 4815,
            Seats = 146,
            CargoCapacityKg = 15520,
            Family = families.First(f => f.Id == 5)
        },
        new()
        {
            Id = 8,
            ModelName = "CRJ900",
            RangeKm = 3200,
            Seats = 90,
            CargoCapacityKg = 12900,
            Family = families.First(f => f.Id == 6)
        },
        new()
        {
            Id = 9,
            ModelName = "C919",
            RangeKm = 5500,
            Seats = 168,
            CargoCapacityKg = 21000,
            Family = families.First(f => f.Id == 7)
        },
        new()
        {
            Id = 10,
            ModelName = "Tu-204-100CE",
            RangeKm = 6300,
            Seats = 214,
            CargoCapacityKg = 38000,
            Family = families.First(f => f.Id == 8)
        }
    };

    /// <summary>
    /// Получить начальные рейсы.
    /// </summary>
    public static List<Flight> GetFlights(List<AircraftModel> models) => new()
    {
        new()
        {
            Id = 1,
            Code = "SU100",
            From = "SVO",
            To = "LED",
            DateOfDeparture = new DateOnly(2025, 10, 21),
            DateOfArrival = new DateOnly(2025, 10, 21),
            TimeOfDeparture = new TimeOnly(10, 0),
            FlightDuration = new TimeSpan(1, 0, 0),
            Model = models.First(m => m.Id == 1)
        },
        new()
        {
            Id = 2,
            Code = "SU101",
            From = "LED",
            To = "SVO",
            DateOfDeparture = new DateOnly(2025, 10, 21),
            DateOfArrival = new DateOnly(2025, 10, 21),
            TimeOfDeparture = new TimeOnly(14, 30),
            FlightDuration = new TimeSpan(1, 0, 0),
            Model = models.First(m => m.Id == 2)
        },
        new()
        {
            Id = 3,
            Code = "SU102",
            From = "SVO",
            To = "AER",
            DateOfDeparture = new DateOnly(2025, 10, 22),
            DateOfArrival = new DateOnly(2025, 10, 22),
            TimeOfDeparture = new TimeOnly(11, 0),
            FlightDuration = new TimeSpan(2, 30, 0),
            Model = models.First(m => m.Id == 3)
        },
        new()
        {
            Id = 4,
            Code = "SU103",
            From = "AER",
            To = "SVO",
            DateOfDeparture = new DateOnly(2025, 10, 22),
            DateOfArrival = new DateOnly(2025, 10, 22),
            TimeOfDeparture = new TimeOnly(15, 0),
            FlightDuration = new TimeSpan(2, 30, 0),
            Model = models.First(m => m.Id == 4)
        },
        new()
        {
            Id = 5,
            Code = "SU104",
            From = "SVX",
            To = "SVO",
            DateOfDeparture = new DateOnly(2025, 10, 23),
            DateOfArrival = new DateOnly(2025, 10, 23),
            TimeOfDeparture = new TimeOnly(18, 0),
            FlightDuration = new TimeSpan(2, 0, 0),
            Model = models.First(m => m.Id == 5)
        },
        new()
        {
            Id = 6,
            Code = "SU105",
            From = "SVO",
            To = "SVX",
            DateOfDeparture = new DateOnly(2025, 10, 23),
            DateOfArrival = new DateOnly(2025, 10, 24),
            TimeOfDeparture = new TimeOnly(7, 0),
            FlightDuration = new TimeSpan(2, 0, 0),
            Model = models.First(m => m.Id == 1)
        },
        new()
        {
            Id = 7,
            Code = "SU106",
            From = "LED",
            To = "SVO",
            DateOfDeparture = new DateOnly(2025, 10, 24),
            DateOfArrival = new DateOnly(2025, 10, 24),
            TimeOfDeparture = new TimeOnly(12, 0),
            FlightDuration = new TimeSpan(1, 0, 0),
            Model = models.First(m => m.Id == 2)
        },
        new()
        {
            Id = 8,
            Code = "SU107",
            From = "SVO",
            To = "LED",
            DateOfDeparture = new DateOnly(2025, 10, 24),
            DateOfArrival = new DateOnly(2025, 10, 24),
            TimeOfDeparture = new TimeOnly(16, 0),
            FlightDuration = new TimeSpan(1, 0, 0),
            Model = models.First(m => m.Id == 3)
        },
        new()
        {
            Id = 9,
            Code = "SU108",
            From = "VKO",
            To = "LED",
            DateOfDeparture = new DateOnly(2025, 10, 25),
            DateOfArrival = new DateOnly(2025, 10, 25),
            TimeOfDeparture = new TimeOnly(9, 30),
            FlightDuration = new TimeSpan(1, 30, 0),
            Model = models.First(m => m.Id == 4)
        },
        new()
        {
            Id = 10,
            Code = "SU109",
            From = "LED",
            To = "VKO",
            DateOfDeparture = new DateOnly(2025, 10, 25),
            DateOfArrival = new DateOnly(2025, 10, 25),
            TimeOfDeparture = new TimeOnly(13, 0),
            FlightDuration = new TimeSpan(1, 30, 0),
            Model = models.First(m => m.Id == 5)
        }
    };

    /// <summary>
    /// Получить начальных пассажиров.
    /// </summary>
    public static List<Passenger> GetPassengers() => new()
    {
        new()
        {
            Id = 1,
            FirstName = "Иван",
            LastName = "Иванов",
            Patronymic = "Иванович",
            PassportNumber = "7701234567",
            BirthDate = new DateOnly(1985, 5, 15)
        },
        new()
        {
            Id = 2,
            FirstName = "Петр",
            LastName = "Петров",
            Patronymic = "Петрович",
            PassportNumber = "7702345678",
            BirthDate = new DateOnly(1990, 3, 20)
        },
        new()
        {
            Id = 3,
            FirstName = "Мария",
            LastName = "Сидорова",
            Patronymic = "Сергеевна",
            PassportNumber = "7703456789",
            BirthDate = new DateOnly(1988, 7, 10)
        },
        new()
        {
            Id = 4,
            FirstName = "Анна",
            LastName = "Васильева",
            Patronymic = "Алексеевна",
            PassportNumber = "7704567890",
            BirthDate = new DateOnly(1992, 1, 25)
        },
        new()
        {
            Id = 5,
            FirstName = "Сергей",
            LastName = "Смирнов",
            Patronymic = "Викторович",
            PassportNumber = "7705678901",
            BirthDate = new DateOnly(1987, 9, 5)
        },
        new()
        {
            Id = 6,
            FirstName = "Елена",
            LastName = "Морозова",
            Patronymic = "Ивановна",
            PassportNumber = "7706789012",
            BirthDate = new DateOnly(1991, 11, 30)
        },
        new()
        {
            Id = 7,
            FirstName = "Олег",
            LastName = "Соколов",
            Patronymic = "Николаевич",
            PassportNumber = "7707890123",
            BirthDate = new DateOnly(1989, 4, 12)
        },
        new()
        {
            Id = 8,
            FirstName = "Татьяна",
            LastName = "Волкова",
            Patronymic = "Дмитриевна",
            PassportNumber = "7708901234",
            BirthDate = new DateOnly(1993, 6, 18)
        },
        new()
        {
            Id = 9,
            FirstName = "Дмитрий",
            LastName = "Соколов",
            Patronymic = "Геннадьевич",
            PassportNumber = "7709012345",
            BirthDate = new DateOnly(1986, 8, 22)
        },
        new()
        {
            Id = 10,
            FirstName = "Нина",
            LastName = "Кузнецова",
            Patronymic = "Викторовна",
            PassportNumber = "7710123456",
            BirthDate = new DateOnly(1994, 2, 8)
        }
    };

    /// <summary>
    /// Получить начальные билеты.
    /// </summary>
    public static List<Ticket> GetTickets(List<Flight> flights, List<Passenger> passengers) => new()
    {
        new()
        {
            Id = 1,
            Flight = flights.First(f => f.Id == 1),
            Passenger = passengers.First(p => p.Id == 1),
            SeatId = "1A",
            HasCarryOn = true,
            BaggageKg = 20
        },
        new()
        {
            Id = 2,
            Flight = flights.First(f => f.Id == 1),
            Passenger = passengers.First(p => p.Id == 2),
            SeatId = "1B",
            HasCarryOn = false,
            BaggageKg = 0
        },
        new()
        {
            Id = 3,
            Flight = flights.First(f => f.Id == 1),
            Passenger = passengers.First(p => p.Id == 3),
            SeatId = "1C",
            HasCarryOn = true,
            BaggageKg = 15
        },
        new()
        {
            Id = 4,
            Flight = flights.First(f => f.Id == 2),
            Passenger = passengers.First(p => p.Id == 4),
            SeatId = "2A",
            HasCarryOn = true,
            BaggageKg = 25
        },
        new()
        {
            Id = 5,
            Flight = flights.First(f => f.Id == 2),
            Passenger = passengers.First(p => p.Id == 5),
            SeatId = "2B",
            HasCarryOn = false,
            BaggageKg = 0
        },
        new()
        {
            Id = 6,
            Flight = flights.First(f => f.Id == 3),
            Passenger = passengers.First(p => p.Id == 6),
            SeatId = "3A",
            HasCarryOn = true,
            BaggageKg = 30
        },
        new()
        {
            Id = 7,
            Flight = flights.First(f => f.Id == 4),
            Passenger = passengers.First(p => p.Id == 7),
            SeatId = "4A",
            HasCarryOn = false,
            BaggageKg = 0
        },
        new()
        {
            Id = 8,
            Flight = flights.First(f => f.Id == 5),
            Passenger = passengers.First(p => p.Id == 8),
            SeatId = "5A",
            HasCarryOn = true,
            BaggageKg = 22
        },
        new()
        {
            Id = 9,
            Flight = flights.First(f => f.Id == 6),
            Passenger = passengers.First(p => p.Id == 9),
            SeatId = "6A",
            HasCarryOn = true,
            BaggageKg = 18
        },
        new()
        {
            Id = 10,
            Flight = flights.First(f => f.Id == 7),
            Passenger = passengers.First(p => p.Id == 10),
            SeatId = "7A",
            HasCarryOn = false,
            BaggageKg = 0
        }
    };
}
