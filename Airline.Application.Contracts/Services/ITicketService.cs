using Airline.Application.Contracts.Tickets;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Специализированный интерфейс сервиса билетов с дополнительными методами.
/// </summary>
public interface ITicketService : IApplicationService<TicketDto, TicketCreateUpdateDto>
{
    /// <summary>
    /// Получить все билеты для указанного рейса.
    /// </summary>
    /// <param name="flightId">Идентификатор рейса.</param>
    /// <returns>Список DTO билетов, принадлежащих указанному рейсу.</returns>
    public Task<List<TicketDto>> GetTicketsByFlightIdAsync(int flightId);
}
