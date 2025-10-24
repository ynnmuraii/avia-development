using Xunit;
using Airline.Domain;

namespace Airline.Tests;

/// <summary>
/// Набор тестов для проверки аналитических запросов авиакомпании.
/// </summary>
public class AirlineQueriesTests(AirlineData data) : IClassFixture<AirlineData>
{
    /// <summary>
    /// Проверяет, что рейсы с минимальной длительностью корректно определяются.
    /// </summary>
    [Fact]
    public void GetFlightsWithMinimalDuration_ReturnsExpectedFlights()
    {
        var minDuration = data.Flights.Min(f => f.FlightDuration);
        var shortestFlights = data.Flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        Assert.NotEmpty(shortestFlights);
        Assert.Contains(shortestFlights, f => f.FlightDuration == minDuration);
    }

    /// <summary>
    /// Проверяет, что топ-5 авиарейсов по числу пассажиров определяются корректно.
    /// </summary>
    [Fact]
    public void GetTop5FlightsByPassengerCount_ReturnsFiveFlights()
    {
        var topFlights = data.Tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, PassengerCount = g.Count() })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topFlights);
        Assert.Equal(5, topFlights.Count);
        Assert.Contains(topFlights, x => x.PassengerCount > 0);
    }

    /// <summary>
    /// Проверяет, что пассажиры без багажа корректно определяются для выбранных рейсов.
    /// </summary>
    [Theory]
    [InlineData("SU100")]
    [InlineData("SU106")]
    [InlineData("SU108")]
    public void GetPassengersWithoutBaggage_ReturnsPassengers(string flightCode)
    {
        var flight = data.Flights.FirstOrDefault(f => f.Code == flightCode);
        Assert.NotNull(flight);
        
        var passengers = data.Tickets
            .Where(t => t.Flight == flight && t.BaggageKg == 0)
            .Select(t => t.Passenger)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToList();

        Assert.NotEmpty(passengers);
        Assert.True(passengers.SequenceEqual(passengers.OrderBy(p => p.LastName).ThenBy(p => p.FirstName)));

    }

    /// <summary>
    /// Проверяет, что полёты выбранной модели за указанный период времени корректно определяются.
    /// </summary>
    [Fact]
    public void GetFlightsByModelAndDate_ReturnsCorrectFlights()
    {
        var model = data.Models.First();
        var startDate = new DateOnly(2025, 10, 21);
        var endDate = new DateOnly(2025, 10, 27);

        var flights = data.Flights
            .Where(f => f.Model == model &&
                        f.DateOfDeparture >= startDate &&
                        f.DateOfDeparture <= endDate)
            .ToList();

        Assert.NotEmpty(flights);
        Assert.Contains(flights, f => f.Model == model &&
                                      f.DateOfDeparture >= startDate &&
                                      f.DateOfDeparture <= endDate);
    }


    /// <summary>
    /// Проверяет, что рейсы из заданного пункта отправления в пункт прибытия корректно определяются.
    /// </summary>
    [Fact]
    public void GetFlightsFromLEDToSVO_ReturnsExpectedFlights()
    {
        var from = "LED";
        var to = "SVO";
        
        var flights = data.Flights
            .Where(f => f.From == from && f.To == to)
            .ToList();

        Assert.NotEmpty(flights);
        Assert.Contains(flights, f => f.From == from && f.To == to);
    }
}
