namespace Airline.Domain.Data;

/// <summary>
/// Класс для хранения данных авиакомпании, включающий информацию о самолетах, рейсах, пассажирах и билетах.
/// </summary>
public class AirlineData
{
    /// <summary>
    /// Список семейств самолётов.
    /// </summary>
    public List<AircraftFamily> Families { get; set; }

    /// <summary>
    /// Список моделей самолётов.
    /// </summary>
    public List<AircraftModel> Models { get; set; }

    /// <summary>
    /// Список рейсов.
    /// </summary>
    public List<Flight> Flights { get; set; }

    /// <summary>
    /// Список пассажироа.
    /// </summary>
    public List<Passenger> Passengers { get; set; }

    /// <summary>
    /// Список билетов.
    /// </summary>
    public List<Ticket> Tickets { get; set; }

    /// <summary>
    /// Инициализация тестовых данных авиакомпании.
    /// </summary>
    public AirlineData()
    {
        Families = new()
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

        Models = new()
        {
            new()
            {
                Id = 1,
                ModelName = "A320neo",
                RangeKm = 6300,
                Seats = 180,
                CargoCapacityKg = 4700,
                Family = Families[0]
            },
            new()
            {
                Id = 2,
                ModelName = "737 MAX 8",
                RangeKm = 5500,
                Seats = 175,
                CargoCapacityKg = 5000,
                Family = Families[1]
            },
            new()
            {
                Id = 3,
                ModelName = "Superjet 100-95",
                RangeKm = 3000,
                Seats = 98,
                CargoCapacityKg = 2600,
                Family = Families[2]
            },
            new()
            {
                Id = 4,
                ModelName = "MC-21-300",
                RangeKm = 6000,
                Seats = 163,
                CargoCapacityKg = 4600,
                Family = Families[3]
            },
            new()
            {
                Id = 5,
                ModelName = "E195-E2",
                RangeKm = 4800,
                Seats = 132,
                CargoCapacityKg = 3300,
                Family = Families[4]
            },
            new()
            {
                Id = 6,
                ModelName = "CRJ-900",
                RangeKm = 2900,
                Seats = 90,
                CargoCapacityKg = 2200,
                Family = Families[5]
            },
            new()
            {
                Id = 7,
                ModelName = "C919",
                RangeKm = 4075,
                Seats = 168,
                CargoCapacityKg = 4100,
                Family = Families[6]
            },
            new()
            {
                Id = 8,
                ModelName = "Tu-204SM",
                RangeKm = 4300,
                Seats = 210,
                CargoCapacityKg = 5800,
                Family = Families[7]
            },
            new()
            {
                Id = 9,
                ModelName = "Il-114-300",
                RangeKm = 1900,
                Seats = 68,
                CargoCapacityKg = 1900,
                Family = Families[8]
            },
            new()
            {
                Id = 10,
                ModelName = "Dash 8-Q400",
                RangeKm = 2500,
                Seats = 78,
                CargoCapacityKg = 2300,
                Family = Families[9]
            }
        };

        Passengers = new()
        {
            new()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Сергеевич",
                PassportNumber = "4510 123456",
                BirthDate = new(1988, 5, 4)
            },
            new()
            {
                Id = 2,
                FirstName = "Пётр",
                LastName = "Петров",
                Patronymic = "Алексеевич",
                PassportNumber = "5002 654321",
                BirthDate = new(1990, 8, 19)
            },
            new()
            {
                Id = 3,
                FirstName = "Анна",
                LastName = "Сидорова",
                Patronymic = "",
                PassportNumber = "4509 789012",
                BirthDate = new(1995, 1, 14)
            },
            new()
            {
                Id = 4,
                FirstName = "Николай",
                LastName = "Морозов",
                Patronymic = "Павлович",
                PassportNumber = "4701 345678",
                BirthDate = new(1982, 7, 22)
            },
            new()
            {
                Id = 5,
                FirstName = "Елена",
                LastName = "Кузнецова",
                Patronymic = "",
                PassportNumber = "4303 567890",
                BirthDate = new(1999, 10, 3)
            },
            new()
            {
                Id = 6,
                FirstName = "Мария",
                LastName = "Орлова",
                Patronymic = "Петровна",
                PassportNumber = "4011 432198",
                BirthDate = new(1975, 2, 11)
            },
            new()
            {
                Id = 7,
                FirstName = "Артём",
                LastName = "Егоров",
                Patronymic = "Андреевич",
                PassportNumber = "4215 112233",
                BirthDate = new(1993, 11, 15)
            },
            new()
            {
                Id = 8,
                FirstName = "Дарья",
                LastName = "Волкова",
                Patronymic = "Владимировна",
                PassportNumber = "4004 998877",
                BirthDate = new(1987, 3, 9)
            },
            new()
            {
                Id = 9,
                FirstName = "Павел",
                LastName = "Смирнов",
                Patronymic = "",
                PassportNumber = "4507 445566",
                BirthDate = new(1991, 6, 27)
            },
            new()
            {
                Id = 10,
                FirstName = "Алина",
                LastName = "Громова",
                Patronymic = "Сергеевна",
                PassportNumber = "4709 667788",
                BirthDate = new(2001, 9, 5)
            }
        };

        Flights = new()
        {
            new()
            {
                Id = 1,
                Code = "SU100",
                From = "SVO",
                To = "LED",
                DateOfDeparture = new(2025, 10, 21),
                DateOfArrival = new(2025, 10, 21),
                TimeOfDeparture = new(8, 30),
                FlightDuration = TimeSpan.FromHours(1.2),
                Model = Models[0]
            },
            new()
            {
                Id = 2,
                Code = "SU101",
                From = "SVO",
                To = "KZN",
                DateOfDeparture = new(2025, 10, 22),
                DateOfArrival = new(2025, 10, 22),
                TimeOfDeparture = new(14, 00),
                FlightDuration = TimeSpan.FromHours(1.5),
                Model = Models[1]
            },
            new()
            {
                Id = 3,
                Code = "SU102",
                From = "LED",
                To = "SVO",
                DateOfDeparture = new(2025, 10, 23),
                DateOfArrival = new(2025, 10, 23),
                TimeOfDeparture = new(9, 45),
                FlightDuration = TimeSpan.FromHours(1.3),
                Model = Models[2]
            },
            new()
            {
                Id = 4,
                Code = "SU103",
                From = "VKO",
                To = "KUF",
                DateOfDeparture = new(2025, 10, 23),
                DateOfArrival = new(2025, 10, 23),
                TimeOfDeparture = new(11, 15),
                FlightDuration = TimeSpan.FromHours(2),
                Model = Models[3]
            },
            new()
            {
                Id = 5,
                Code = "SU104",
                From = "KZN",
                To = "LED",
                DateOfDeparture = new(2025, 10, 24),
                DateOfArrival = new(2025, 10, 24),
                TimeOfDeparture = new(7, 50),
                FlightDuration = TimeSpan.FromHours(2.2),
                Model = Models[4]
            },
            new()
            {
                Id = 6,
                Code = "SU105",
                From = "DME",
                To = "KUF",
                DateOfDeparture = new(2025, 10, 25),
                DateOfArrival = new(2025, 10, 25),
                TimeOfDeparture = new(13, 40),
                FlightDuration = TimeSpan.FromHours(1.8),
                Model = Models[5]
            },
            new()
            {
                Id = 7,
                Code = "SU106",
                From = "LED",
                To = "SVO",
                DateOfDeparture = new(2025, 10, 26),
                DateOfArrival = new(2025, 10, 26),
                TimeOfDeparture = new(15, 10),
                FlightDuration = TimeSpan.FromHours(1.3),
                Model = Models[6]
            },
            new()
            {
                Id = 8,
                Code = "SU107",
                From = "SVO",
                To = "AER",
                DateOfDeparture = new(2025, 10, 27),
                DateOfArrival = new(2025, 10, 27),
                TimeOfDeparture = new(16, 00),
                FlightDuration = TimeSpan.FromHours(2.1),
                Model = Models[7]
            },
            new()
            {
                Id = 9,
                Code = "SU108",
                From = "SVO",
                To = "SVX",
                DateOfDeparture = new(2025, 10, 28),
                DateOfArrival = new(2025, 10, 28),
                TimeOfDeparture = new(12, 15),
                FlightDuration = TimeSpan.FromHours(2.3),
                Model = Models[8]
            },
            new()
            {
                Id = 10,
                Code = "SU109",
                From = "LED",
                To = "KZN",
                DateOfDeparture = new(2025, 10, 29),
                DateOfArrival = new(2025, 10, 29),
                TimeOfDeparture = new(6, 50),
                FlightDuration = TimeSpan.FromHours(2.4),
                Model = Models[9]
            }
        };

        Tickets = new()
        {
            new()
            {
                Id = 1,
                Flight = Flights[0],
                Passenger = Passengers[1],
                SeatId = "12B",
                HasCarryOn = true,
                BaggageKg = 0
            },
            new()
            {
                Id = 2,
                Flight = Flights[1],
                Passenger = Passengers[2],
                SeatId = "8C",
                HasCarryOn = false,
                BaggageKg = 25
            },
            new()
            {
                Id = 3,
                Flight = Flights[2],
                Passenger = Passengers[3],
                SeatId = "3A",
                HasCarryOn = true,
                BaggageKg = 10
            },
            new()
            {
                Id = 4,
                Flight = Flights[3],
                Passenger = Passengers[4],
                SeatId = "5D",
                HasCarryOn = false,
                BaggageKg = 18
            },
            new()
            {
                Id = 5,
                Flight = Flights[4],
                Passenger = Passengers[5],
                SeatId = "10A",
                HasCarryOn = true,
                BaggageKg = 22
            },
            new()
            {
                Id = 6,
                Flight = Flights[5],
                Passenger = Passengers[6],
                SeatId = "14B",
                HasCarryOn = true,
                BaggageKg = 12
            },
            new()
            {
                Id = 7,
                Flight = Flights[6],
                Passenger = Passengers[7],
                SeatId = "1C",
                HasCarryOn = false,
                BaggageKg = 0
            },
            new()
            {
                Id = 8,
                Flight = Flights[7],
                Passenger = Passengers[8],
                SeatId = "9A",
                HasCarryOn = true,
                BaggageKg = 30
            },
            new()
            {
                Id = 9,
                Flight = Flights[8],
                Passenger = Passengers[9],
                SeatId = "11D",
                HasCarryOn = true,
                BaggageKg = 0
            },
            new()
            {
                Id = 10,
                Flight = Flights[9],
                Passenger = Passengers[0],
                SeatId = "12A",
                HasCarryOn = true,
                BaggageKg = 0
            }
        };
    }
}
