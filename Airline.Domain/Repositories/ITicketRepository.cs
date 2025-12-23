namespace Airline.Domain.Repositories;

/// <summary>
/// Специализированный интерфейс репозитория для билетов с дополнительными методами фильтрации.
/// </summary>
public interface ITicketRepository : IRepository<Ticket>
{
    /// <summary>
    /// Получить все билеты для указанного рейса.
    /// </summary>
    /// <param name="flightId">Идентификатор рейса.</param>
    /// <returns>Список билетов, принадлежащих указанному рейсу.</returns>
    Task<IEnumerable<Ticket>> GetByFlightIdAsync(int flightId);
}
