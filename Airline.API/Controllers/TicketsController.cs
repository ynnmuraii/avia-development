using Airline.Application.Contracts.Tickets;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления билетами.
/// </summary>
public class TicketsController(ITicketService service, ILogger<TicketsController> logger) : CrudControllerBase<TicketDto, TicketCreateUpdateDto>(service, logger)
{
}

