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
    /// <param name="flightRepository">Репозиторий рейсов.</param>
    /// <param name="ticketRepository">Репозиторий билетов.</param>
    public AnalyticsService(IRepository<Flight> flightRepository, IRepository<Ticket> ticketRepository)
    {
        _flightRepository = flightRepository;
        _ticketRepository = ticketRepository;
    }

    /// <summary>
    /// Получить рейсы с минимальной длительностью.
    /// </summary>
    public async Task<IEnumerable<dynamic>> GetFlightsWithMinimalDurationAsync()
    {
        var flights = await _flightRepository.ReadAsync();
        var minDuration = flights.Min(f => f.FlightDuration);
        var shortestFlights = flights
            .Where(f => f.FlightDuration == minDuration)
            .Select(f => new { f.Id, f.Code, f.From, f.To, f.FlightDuration })
            .ToList();

        return shortestFlights;
    }

    /// <summary>
    /// Получить топ-5 рейсов по количеству пассажиров.
    /// </summary>
    public async Task<IEnumerable<dynamic>> GetTop5FlightsByPassengerCountAsync()
    {
        var tickets = await _ticketRepository.ReadAsync();
        var topFlights = tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, PassengerCount = g.Count() })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .Select(x => new { x.Flight.Id, x.Flight.Code, x.Flight.From, x.Flight.To, x.PassengerCount })
            .ToList();

        return topFlights;
    }

    /// <summary>
    /// Получить пассажиров без багажа для выбранного рейса.
    /// </summary>
    public async Task<IEnumerable<dynamic>> GetPassengersWithoutBaggageAsync(string flightCode)
    {
        var flights = await _flightRepository.ReadAsync();
        var tickets = await _ticketRepository.ReadAsync();
        
        var flight = flights.FirstOrDefault(f => f.Code == flightCode);
        if (flight is null)
            return Enumerable.Empty<dynamic>();

        var passengers = tickets
            .Where(t => t.Flight == flight && t.BaggageKg == 0)
            .Select(t => t.Passenger)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Select(p => new { p.Id, p.FirstName, p.LastName, p.Patronymic, p.PassportNumber })
            .ToList();

        return passengers;
    }

    /// <summary>
    /// Получить рейсы выбранной модели за указанный период.
    /// </summary>
    public async Task<IEnumerable<dynamic>> GetFlightsByModelAndDateAsync(int modelId, DateOnly startDate, DateOnly endDate)
    {
        var flights = await _flightRepository.ReadAsync();
        
        var flightsByModel = flights
            .Where(f => f.Model.Id == modelId &&
                        f.DateOfDeparture >= startDate &&
                        f.DateOfDeparture <= endDate)
            .Select(f => new { f.Id, f.Code, f.From, f.To, f.DateOfDeparture, f.Model.ModelName })
            .ToList();

        return flightsByModel;
    }

    /// <summary>
    /// Получить рейсы из пункта отправления в пункт прибытия.
    /// </summary>
    public async Task<IEnumerable<dynamic>> GetFlightsByRouteAsync(string from, string to)
    {
        var flights = await _flightRepository.ReadAsync();
        
        var flightsByRoute = flights
            .Where(f => f.From == from && f.To == to)
            .Select(f => new { f.Id, f.Code, f.From, f.To, f.DateOfDeparture, f.TimeOfDeparture })
            .ToList();

        return flightsByRoute;
    }
}
