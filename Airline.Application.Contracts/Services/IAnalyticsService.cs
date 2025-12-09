namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса аналитики авиакомпании.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить рейсы с минимальной длительностью.
    /// </summary>
    /// <returns>Список рейсов с минимальной длительностью.</returns>
    public Task<IEnumerable<dynamic>> GetFlightsWithMinimalDurationAsync();

    /// <summary>
    /// Получить топ-5 рейсов по количеству пассажиров.
    /// </summary>
    /// <returns>Список из 5 рейсов с наибольшим числом пассажиров.</returns>
    public Task<IEnumerable<dynamic>> GetTop5FlightsByPassengerCountAsync();

    /// <summary>
    /// Получить пассажиров без багажа для выбранного рейса.
    /// </summary>
    /// <param name="flightCode">Код рейса.</param>
    /// <returns>Список пассажиров без багажа, отсортированный по фамилии и имени.</returns>
    public Task<IEnumerable<dynamic>> GetPassengersWithoutBaggageAsync(string flightCode);

    /// <summary>
    /// Получить рейсы выбранной модели за указанный период.
    /// </summary>
    /// <param name="modelId">Идентификатор модели самолёта.</param>
    /// <param name="startDate">Дата начала периода.</param>
    /// <param name="endDate">Дата конца периода.</param>
    /// <returns>Список рейсов, соответствующих условиям.</returns>
    public Task<IEnumerable<dynamic>> GetFlightsByModelAndDateAsync(int modelId, DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Получить рейсы из пункта отправления в пункт прибытия.
    /// </summary>
    /// <param name="from">Код аэропорта отправления.</param>
    /// <param name="to">Код аэропорта прибытия.</param>
    /// <returns>Список рейсов по маршруту.</returns>
    public Task<IEnumerable<dynamic>> GetFlightsByRouteAsync(string from, string to);
}
