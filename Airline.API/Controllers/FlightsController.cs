using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;
using Airline.Application.Contracts.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления рейсами.
/// </summary>
public class FlightsController(
    IApplicationService<FlightDto, FlightCreateUpdateDto> flightService,
    ITicketService ticketService,
    ILogger<FlightsController> logger) : CrudControllerBase<FlightDto, FlightCreateUpdateDto>(flightService, logger)
{
    /// <summary>
    /// Получить все билеты для указанного рейса.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <returns>Список билетов рейса или 404, если рейс не найден.</returns>
    [HttpGet("{id}/tickets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetFlightTickets(int id)
    {
        logger.LogInformation("{method} method called for flight id={id}", nameof(GetFlightTickets), id);
        try
        {
            var flight = await Service.GetByIdAsync(id);
            if (flight == null)
                return NotFound($"Flight with id={id} not found");

            var tickets = await ticketService.GetTicketsByFlightIdAsync(id);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(GetFlightTickets), ex);
            return StatusCode(500, ex.Message);
        }
    }
}

