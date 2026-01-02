using Airline.Application.Contracts.Analytics;
using Airline.Application.Contracts.Passengers;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса аналитики авиакомпании.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить рейсы с минимальной длительностью.
    /// </summary>
    public Task<IEnumerable<FlightWithMinimalDurationDto>> GetFlightsWithMinimalDurationAsync();

    /// <summary>
    /// Получить топ-5 рейсов по количеству пассажиров.
    /// </summary>
    public Task<IEnumerable<FlightWithCountDto>> GetTop5FlightsByPassengerCountAsync();

    /// <summary>
    /// Получить пассажиров без багажа для выбранного рейса.
    /// </summary>
    public Task<IEnumerable<PassengerDto>> GetPassengersWithoutBaggageAsync(string flightCode);

    /// <summary>
    /// Получить рейсы выбранной модели за указанный период.
    /// </summary>
    public Task<IEnumerable<FlightByModelAndDateDto>> GetFlightsByModelAndDateAsync(int modelId, DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Получить рейсы из пункта отправления в пункт прибытия.
    /// </summary>
    public Task<IEnumerable<FlightByRouteDto>> GetFlightsByRouteAsync(string from, string to);
}
