﻿using Xunit;
using Airline.Application.Services;
using Airline.Domain;
using Airline.Infrastructure.EfCore;
using Airline.Infrastructure.EfCore.Repositories;
using Airline.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Airline.Tests;

public class AnalyticsServiceTests : IDisposable
{
    private readonly AnalyticsService _service;
    private readonly AirlineDbContext _context;

    public AnalyticsServiceTests()
    {
        var options = new DbContextOptionsBuilder<AirlineDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AirlineDbContext(options);
        
        var families = DbInitializer.GetAircraftFamilies();
        var models = DbInitializer.GetAircraftModels(families);
        var flights = DbInitializer.GetFlights(models);
        var passengers = DbInitializer.GetPassengers();
        var tickets = DbInitializer.GetTickets(flights, passengers);

        _context.AircraftFamilies.AddRange(families);
        _context.AircraftModels.AddRange(models);
        _context.Flights.AddRange(flights);
        _context.Passengers.AddRange(passengers);
        _context.Tickets.AddRange(tickets);
        _context.SaveChanges();

        var flightRepo = new FlightRepository(_context);
        var ticketRepo = new TicketRepository(_context);

        
        _service = new AnalyticsService(flightRepo, ticketRepo);
    }

    public void Dispose()
    {
        _context.Dispose();
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