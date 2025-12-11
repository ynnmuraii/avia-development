using Airline.Application.Contracts.Analytics;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис аналитики для авиакомпании.
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IRepository<Flight> _flightRepository;
    private readonly IRepository<Ticket> _ticketRepository;

    /// <summary>
    /// Инициализирует сервис аналитики.
    /// </summary>
    public AnalyticsService(IRepository<Flight> flightRepository, IRepository<Ticket> ticketRepository)
    {
        _flightRepository = flightRepository;
        _ticketRepository = ticketRepository;
    }

    /// <summary>
    /// Получить рейсы с минимальной длительностью.
    /// </summary>
    public async Task<IEnumerable<FlightWithMinimalDurationDto>> GetFlightsWithMinimalDurationAsync()
    {
        var flights = await _flightRepository.ReadAsync();
        if (!flights.Any())
            return Enumerable.Empty<FlightWithMinimalDurationDto>();

        var minDuration = flights.Where(f => f.FlightDuration.HasValue).Min(f => f.FlightDuration?.TotalMinutes ?? int.MaxValue);
        var shortestFlights = flights
            .Where(f => f.FlightDuration.HasValue && f.FlightDuration.Value.TotalMinutes == minDuration)
            .Select(f => new FlightWithMinimalDurationDto
            {
                Id = f.Id,
                Code = f.Code,
                From = f.From,
                To = f.To,
                FlightDuration = (int)(f.FlightDuration?.TotalMinutes ?? 0)
            })
            .ToList();

        return shortestFlights;
    }

    /// <summary>
    /// Получить топ-5 рейсов по количеству пассажиров.
    /// </summary>
    public async Task<IEnumerable<FlightWithCountDto>> GetTop5FlightsByPassengerCountAsync()
    {
        var tickets = await _ticketRepository.ReadAsync();
        var topFlights = tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, PassengerCount = g.Count() })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .Select(x => new FlightWithCountDto
            {
                FlightId = x.Flight.Id,
                FlightCode = x.Flight.Code,
                PassengerCount = x.PassengerCount
            })
            .ToList();

        return topFlights;
    }

    /// <summary>
    /// Получить пассажиров без багажа для выбранного рейса.
    /// </summary>
    public async Task<IEnumerable<PassengerDto>> GetPassengersWithoutBaggageAsync(string flightCode)
    {
        var flights = await _flightRepository.ReadAsync();
        var tickets = await _ticketRepository.ReadAsync();
        
        var flight = flights.FirstOrDefault(f => f.Code == flightCode);
        if (flight is null)
            return Enumerable.Empty<PassengerDto>();

        var passengers = tickets
            .Where(t => t.Flight.Id == flight.Id && t.BaggageKg == 0)
            .Select(t => t.Passenger)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Distinct()
            .Select(p => new PassengerDto(
                p.Id,
                p.FirstName,
                p.LastName,
                p.Patronymic,
                p.PassportNumber,
                p.BirthDate
            ))
            .ToList();

        return passengers;
    }

    /// <summary>
    /// Получить рейсы выбранной модели за указанный период.
    /// </summary>
    public async Task<IEnumerable<FlightByModelAndDateDto>> GetFlightsByModelAndDateAsync(int modelId, DateOnly startDate, DateOnly endDate)
    {
        var flights = await _flightRepository.ReadAsync();
        
        var flightsByModel = flights
            .Where(f => f.Model.Id == modelId &&
                        f.DateOfDeparture.HasValue &&
                        f.DateOfDeparture >= startDate &&
                        f.DateOfDeparture <= endDate)
            .Select(f => new FlightByModelAndDateDto
            {
                Id = f.Id,
                Code = f.Code,
                From = f.From,
                To = f.To,
                DateOfDeparture = f.DateOfDeparture ?? DateOnly.FromDateTime(DateTime.Now),
                ModelName = f.Model.ModelName
            })
            .ToList();

        return flightsByModel;
    }

    /// <summary>
    /// Получить рейсы из пункта отправления в пункт прибытия.
    /// </summary>
    public async Task<IEnumerable<FlightByRouteDto>> GetFlightsByRouteAsync(string from, string to)
    {
        var flights = await _flightRepository.ReadAsync();
        
        var flightsByRoute = flights
            .Where(f => f.From == from && f.To == to)
            .Select(f => new FlightByRouteDto
            {
                Id = f.Id,
                Code = f.Code,
                From = f.From,
                To = f.To,
                DateOfDeparture = f.DateOfDeparture ?? DateOnly.FromDateTime(DateTime.Now),
                TimeOfDeparture = f.TimeOfDeparture ?? TimeOnly.FromDateTime(DateTime.Now)
            })
            .ToList();

        return flightsByRoute;
    }
}
