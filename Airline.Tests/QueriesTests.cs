namespace Airline.Tests;

public class AirlineQueriesTests
{
    [Fact]
    public void Flights_With_Minimal_Duration()
    {
        var data = new AirlineData();

        var minDuration = data.Flights.Min(f => f.FlightDuration);
        var shortestFlights = data.Flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        Assert.NotEmpty(shortestFlights);
        Assert.All(shortestFlights, f => Assert.Equal(minDuration, f.FlightDuration));
    }


    [Fact]
    public void Top5_Flights_by_PassengerCount()
    {
        var data = new AirlineData();

        var topFlights = data.Tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, PassengerCount = g.Count() })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topFlights);
        Assert.Equal(5, topFlights.Count);
        Assert.All(topFlights, x => Assert.True(x.PassengerCount > 0));
    }

    [Fact]
    public void Passenger_Without_Bagagge()
    {
        var data = new AirlineData();

        var passengerWithoutBag = data.Passengers
    }

    [Fact]
    public void AirplaneModel_Flights_by_Date()
    {
        var data = new AirlineData();


    }

    [Fact]
    public void Flights_From_To()
    {
        var data = new AirlineData();


    }
}

