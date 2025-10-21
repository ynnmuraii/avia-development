using System;
using namespace Airline.Domain

public class AirlineData
{
	public List<AircraftFamily> Families { get; set; }
	public List<AircraftModel> Models { get; set; }
    public List<Flight> Flights { get; set; } 
    public List<Passenger> Passengers { get; set; }
    public List<Ticket> Tickets { get; set; }

	public AirlineData()
	{
        Families = new List<AircraftFamily>
        {
            new() { Manufacturer = "Airbus", FamilyName = "A320" },
            new() { Manufacturer = "Boeing", FamilyName = "737" },
            new() { Manufacturer = "Sukhoi", FamilyName = "Superjet 100" },
            new() { Manufacturer = "Irkut", FamilyName = "MC-21" },
            new() { Manufacturer = "Embraer", FamilyName = "E-Jet E2" },
            new() { Manufacturer = "Bombardier", FamilyName = "CRJ" },
            new() { Manufacturer = "Comac", FamilyName = "C919" },
            new() { Manufacturer = "Tupolev", FamilyName = "Tu-204" },
            new() { Manufacturer = "Ilyushin", FamilyName = "Il-114" },
            new() { Manufacturer = "De Havilland Canada", FamilyName = "Dash 8" }
        };

        Models = new List<AircraftModel>
        {
            new() { ModelName = "A320neo", RangeKm = 6300, Seats = 180, Family = Families[0] },
            new() { ModelName = "737 MAX 8", RangeKm = 5500, Seats = 175, Family = Families[1] },
            new() { ModelName = "Superjet 100-95", RangeKm = 3000, Seats = 98, Family = Families[2] },
            new() { ModelName = "MC-21-300", RangeKm = 6000, Seats = 163, Family = Families[3] },
            new() { ModelName = "E195-E2", RangeKm = 4800, Seats = 132, Family = Families[4] },
            new() { ModelName = "CRJ-900", RangeKm = 2900, Seats = 90, Family = Families[5] },
            new() { ModelName = "C919", RangeKm = 4075, Seats = 168, Family = Families[6] },
            new() { ModelName = "Tu-204SM", RangeKm = 4300, Seats = 210, Family = Families[7] },
            new() { ModelName = "Il-114-300", RangeKm = 1900, Seats = 68, Family = Families[8] },
            new() { ModelName = "Dash 8-Q400", RangeKm = 2500, Seats = 78, Family = Families[9] }
        };

        Passengers = new List<Passenger>
        {
            new() { FirstName = "Иван", LastName = "Иванов", Patronymic = "Сергеевич", PassportNumber = "4510 123456", 
                BirthDate = new DateOnly(1988, 5, 4) },
            new() { FirstName = "Пётр", LastName = "Петров", Patronymic = "Алексеевич", PassportNumber = "5002 654321",
                BirthDate = new DateOnly(1990, 8, 19) },
            new() { FirstName = "Анна", LastName = "Сидорова", Patronymic = "", PassportNumber = "4509 789012", 
                BirthDate = new DateOnly(1995, 1, 14) },
            new() { FirstName = "Николай", LastName = "Морозов", Patronymic = "Павлович", PassportNumber = "4701 345678", 
                BirthDate = new DateOnly(1982, 7, 22) },
            new() { FirstName = "Елена", LastName = "Кузнецова", Patronymic = "", PassportNumber = "4303 567890", 
                BirthDate = new DateOnly(1999, 10, 3) },
            new() { FirstName = "Мария", LastName = "Орлова", Patronymic = "Петровна", PassportNumber = "4011 432198", 
                BirthDate = new DateOnly(1975, 2, 11) },
            new() { FirstName = "Артём", LastName = "Егоров", Patronymic = "Андреевич", PassportNumber = "4215 112233", 
                BirthDate = new DateOnly(1993, 11, 15) },
            new() { FirstName = "Дарья", LastName = "Волкова", Patronymic = "Владимировна", PassportNumber = "4004 998877", 
                BirthDate = new DateOnly(1987, 3, 9) },
            new() { FirstName = "Павел", LastName = "Смирнов", Patronymic = "", PassportNumber = "4507 445566", 
                BirthDate = new DateOnly(1991, 6, 27) },
            new() { FirstName = "Алина", LastName = "Громова", Patronymic = "Сергеевна", PassportNumber = "4709 667788", 
                BirthDate = new DateOnly(2001, 9, 5) }
        };


        Flights = new List<Flight>
        {
            new() { Code = "SU100", From = "SVO", To = "LED", DateOfDeparture = new DateOnly(2025, 10, 21),
                DateOfArrival = new DateOnly(2025, 10, 21), TimeOfDeparture = new TimeOnly(8, 30),
                FlightDuration = TimeSpan.FromHours(1.2), Model = Models[0] },

            new() { Code = "SU101", From = "SVO", To = "KZN", DateOfDeparture = new DateOnly(2025, 10, 22),
                DateOfArrival = new DateOnly(2025, 10, 22), TimeOfDeparture = new TimeOnly(14, 00),
                FlightDuration = TimeSpan.FromHours(1.5), Model = Models[1] },

            new() { Code = "SU102", From = "LED", To = "SVO", DateOfDeparture = new DateOnly(2025, 10, 23),
                DateOfArrival = new DateOnly(2025, 10, 23), TimeOfDeparture = new TimeOnly(9, 45),
                FlightDuration = TimeSpan.FromHours(1.3), Model = Models[2] },

            new() { Code = "SU103", From = "VKO", To = "KUF", DateOfDeparture = new DateOnly(2025, 10, 23),
                DateOfArrival = new DateOnly(2025, 10, 23), TimeOfDeparture = new TimeOnly(11, 15),
                FlightDuration = TimeSpan.FromHours(2), Model = Models[3] },

            new() { Code = "SU104", From = "KZN", To = "LED", DateOfDeparture = new DateOnly(2025, 10, 24),
                DateOfArrival = new DateOnly(2025, 10, 24), TimeOfDeparture = new TimeOnly(7, 50),
                FlightDuration = TimeSpan.FromHours(2.2), Model = Models[4] },

            new() { Code = "SU105", From = "DME", To = "KUF", DateOfDeparture = new DateOnly(2025, 10, 25),
                DateOfArrival = new DateOnly(2025, 10, 25), TimeOfDeparture = new TimeOnly(13, 40),
                FlightDuration = TimeSpan.FromHours(1.8), Model = Models[5] },

            new() { Code = "SU106", From = "LED", To = "SVO", DateOfDeparture = new DateOnly(2025, 10, 26),
                DateOfArrival = new DateOnly(2025, 10, 26), TimeOfDeparture = new TimeOnly(15, 10),
                FlightDuration = TimeSpan.FromHours(1.3), Model = Models[6] },

            new() { Code = "SU107", From = "SVO", To = "AER", DateOfDeparture = new DateOnly(2025, 10, 27),
                DateOfArrival = new DateOnly(2025, 10, 27), TimeOfDeparture = new TimeOnly(16, 00),
                FlightDuration = TimeSpan.FromHours(2.1), Model = Models[7] },

            new() { Code = "SU108", From = "SVO", To = "SVX", DateOfDeparture = new DateOnly(2025, 10, 28),
                DateOfArrival = new DateOnly(2025, 10, 28), TimeOfDeparture = new TimeOnly(12, 15),
                FlightDuration = TimeSpan.FromHours(2.3), Model = Models[8] },

            new() { Code = "SU109", From = "LED", To = "KZN", DateOfDeparture = new DateOnly(2025, 10, 29),
                DateOfArrival = new DateOnly(2025, 10, 29), TimeOfDeparture = new TimeOnly(6, 50),
                FlightDuration = TimeSpan.FromHours(2.4), Model = Models[9] }
        };

        Tickets = new List<Ticket>
        {
            new() { Flight = Flights[0], Passenger = Passengers[1], SeatId = "12B", HasCarryOn = true,  BaggageKg = 20 },
            new() { Flight = Flights[1], Passenger = Passengers[2], SeatId = "8C",  HasCarryOn = false, BaggageKg = 25 },
            new() { Flight = Flights[2], Passenger = Passengers[3], SeatId = "3A",  HasCarryOn = true,  BaggageKg = 10 },
            new() { Flight = Flights[3], Passenger = Passengers[4], SeatId = "5D",  HasCarryOn = false, BaggageKg = 18 },
            new() { Flight = Flights[4], Passenger = Passengers[5], SeatId = "10A", HasCarryOn = true,  BaggageKg = 22 },
            new() { Flight = Flights[5], Passenger = Passengers[6], SeatId = "14B", HasCarryOn = true,  BaggageKg = 12 },
            new() { Flight = Flights[6], Passenger = Passengers[7], SeatId = "1C",  HasCarryOn = false, BaggageKg = 0  },
            new() { Flight = Flights[7], Passenger = Passengers[8], SeatId = "9A",  HasCarryOn = true,  BaggageKg = 30 },
            new() { Flight = Flights[8], Passenger = Passengers[9], SeatId = "11D", HasCarryOn = true,  BaggageKg = 5  },
            new() { Flight = Flights[9], Passenger = Passengers[0], SeatId = "12A", HasCarryOn = true,  BaggageKg = 15 }
        };
    }
}
