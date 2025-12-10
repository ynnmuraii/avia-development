using Xunit;
using Airline.Application.Services;
using Airline.Domain;
using Airline.Infrastructure.InMemory.Repositories;
using Airline.Infrastructure.InMemory.Data;

namespace Airline.Tests;

public class AnalyticsServiceTests
{
    private readonly AnalyticsService _service;

    public AnalyticsServiceTests()
    {
        
        var families = DataSeeder.GetAircraftFamilies();
        var models = DataSeeder.GetAircraftModels(families);
        var flights = DataSeeder.GetFlights(models);
        var passengers = DataSeeder.GetPassengers();
        var tickets = DataSeeder.GetTickets(flights, passengers);

        var flightRepo = new FlightRepository(flights);
        var ticketRepo = new TicketRepository(tickets);

        
        _service = new AnalyticsService(flightRepo, ticketRepo);
    }

    [Fact]
    public async Task GetFlightsWithMinimalDuration_ReturnsCorrectFlight()
    {
        
        var result = await _service.GetFlightsWithMinimalDurationAsync();

        
        Assert.NotEmpty(result);
        var flight = result.First();
        
        Assert.Equal(60, flight.FlightDuration); 
    }

    [Fact]
    public async Task GetTop5FlightsByPassengerCount_ReturnsFlights()
    {
        
        var result = await _service.GetTop5FlightsByPassengerCountAsync();

        
        Assert.NotEmpty(result);
        Assert.True(result.Count() <= 5);
        var list = result.ToList();
        if (list.Count > 1)
        {
            Assert.True(list[0].PassengerCount >= list[1].PassengerCount);
        }
    }
}