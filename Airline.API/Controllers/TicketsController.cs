using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Tickets;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления билетами.
/// </summary>
public class TicketsController : CrudControllerBase<TicketDto, TicketCreateUpdateDto, int>
{
    /// <summary>
    /// Инициализирует контроллер билетов.
    /// </summary>
    public TicketsController(ITicketService service, ILogger<TicketsController> logger)
        : base(service, logger)
    {
    }
}
