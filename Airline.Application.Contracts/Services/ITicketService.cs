using Airline.Application.Contracts.Tickets;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления билетами.
/// </summary>
public interface ITicketService : IApplicationService<TicketDto, TicketCreateUpdateDto, int>
{
}
