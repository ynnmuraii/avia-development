﻿using Xunit;
using Airline.Application.Services;
using Airline.Domain;
using Airline.Infrastructure.InMemory.Data;
using Airline.Infrastructure.InMemory.Repositories;

namespace Airline.Tests;

public class AnalyticsServiceTests
{
    private readonly AnalyticsService _service;
    private readonly List<Flight> _flights;
    private readonly List<Ticket> _tickets;
    private readonly List<Passenger> _passengers;
    private readonly List<AircraftFamily> _families;
    private readonly List<AircraftModel> _models;

    public AnalyticsServiceTests()
    {
        _families = DataSeeder.GetAircraftFamilies();
        _models = DataSeeder.GetAircraftModels(_families);
        _flights = DataSeeder.GetFlights(_models);
        _passengers = DataSeeder.GetPassengers();
        _tickets = DataSeeder.GetTickets(_flights, _passengers);

        var flightRepo = new FlightRepository(_flights);
        var ticketRepo = new TicketRepository(_tickets);

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

    /// <summary>
    /// Проверяет, что пассажиры без багажа корректно определяются для выбранных рейсов.
    /// </summary>
    [Theory]
    [InlineData("SU100")]
    [InlineData("SU106")]
    public void GetPassengersWithoutBaggage_ReturnsPassengers(string flightCode)
    {
        var flight = _flights.FirstOrDefault(f => f.Code == flightCode);
        Assert.NotNull(flight);
        
        var passengers = _tickets
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
        var model = _models.First();
        var startDate = new DateOnly(2025, 10, 21);
        var endDate = new DateOnly(2025, 10, 27);

        var flights = _flights
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
        
        var flights = _flights
            .Where(f => f.From == from && f.To == to)
            .ToList();

        Assert.NotEmpty(flights);
        Assert.Contains(flights, f => f.From == from && f.To == to);
    }
}

