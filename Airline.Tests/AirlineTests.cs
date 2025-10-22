using Xunit;
using Airline.Domain;

namespace Airline.Tests;

/// <summary>
/// Фикстура создаёт общий экземпляр AirlineData, который будет использоваться ввсеми тестами.
/// </summary>
public class AirlineFixture
{
    public AirlineData Data { get; }

    public AirlineFixture()
    {
        Data = new AirlineData();
    }
}

/// <summary>
/// Набор тестов для проверки аналитических запросов авиакомпании.
/// </summary>
public class AirlineQueriesTests : IClassFixture<AirlineFixture>
{
    private readonly AirlineData _data;

    public AirlineQueriesTests(AirlineFixture fixture)
    {
        _data = fixture.Data;
    }

    /// <summary>
    /// Проверяет, что рейсы с минимальной длительностью корректно определяются.
    /// </summary>
    [Fact]
    public void Flights_With_Minimal_Duration()
    {
        var minDuration = _data.Flights.Min(f => f.FlightDuration);
        var shortestFlights = _data.Flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        Assert.NotEmpty(shortestFlights);
        Assert.All(shortestFlights, f => Assert.Equal(minDuration, f.FlightDuration));
    }

    /// <summary>
    /// Проверяет, что топ-5 авиарейсов по числу пассажиров определяются корректно.
    /// </summary>
    [Fact]
    public void Top5_Flights_by_PassengerCount()
    {
        var topFlights = _data.Tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, PassengerCount = g.Count() })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topFlights);
        Assert.Equal(5, topFlights.Count);
        Assert.All(topFlights, x => Assert.True(x.PassengerCount > 0));
    }

    /// <summary>
    /// Проверяет, что пассажиры без багажа корректно определяются для выбранных рейсов.
    /// </summary>
    [Theory]
    [InlineData("SU100")]
    [InlineData("SU106")]
    [InlineData("SU108")]
    public void Passengers_With_No_Baggage_For_Flight(string flightCode)
    {
        var flight = _data.Flights.FirstOrDefault(f => f.Code == flightCode);
        Assert.NotNull(flight);

        var passengers = _data.Tickets
            .Where(t => t.Flight == flight && t.BaggageKg == 0)
            .Select(t => t.Passenger)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToList();

        Assert.NotNull(passengers);
        Assert.All(passengers, p => Assert.NotNull(p));
    }

    /// <summary>
    /// Проверяет, что полёты выбранной модели за указанный период времени корректно определяются.
    /// </summary>
    [Fact]
    public void Flights_By_AircraftModel_In_DateRange()
    {
        var model = _data.Models.First();
        var startDate = new DateOnly(2025, 10, 21);
        var endDate = new DateOnly(2025, 10, 27);

        var flights = _data.Flights
            .Where(f => f.Model == model &&
                        f.DateOfDeparture >= startDate &&
                        f.DateOfDeparture <= endDate)
            .ToList();

        Assert.NotNull(flights);
        Assert.All(flights, f => Assert.Equal(model, f.Model));
    }

    /// <summary>
    /// Проверяет, что рейсы из заданного пункта отправления в пункт прибытия корректно определяются.
    /// </summary>
    [Fact]
    public void Flights_From_Specified_To_Specified_Airports()
    {
        var from = "SVO";
        var to = "LED";

        var flights = _data.Flights
            .Where(f => f.From == from && f.To == to)
            .ToList();

        Assert.NotEmpty(flights);
        Assert.All(flights, f =>
        {
            Assert.Equal(from, f.From);
            Assert.Equal(to, f.To);
        });
    }
}
