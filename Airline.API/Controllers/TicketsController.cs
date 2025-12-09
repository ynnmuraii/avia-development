using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Tickets;
using Airline.Application.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления билетами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    /// <summary>
    /// Инициализирует контроллер билетов.
    /// </summary>
    public TicketsController(TicketService ticketService, ILogger<TicketsController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    /// <summary>
    /// Получить все билеты.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
    {
        var tickets = await _ticketService.GetAllAsync();
        return Ok(tickets);
    }

    /// <summary>
    /// Получить билет по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);
        if (ticket is null)
            return NotFound();

        return Ok(ticket);
    }

    /// <summary>
    /// Создать новый билет.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(TicketCreateUpdateDto createDto)
    {
        var ticket = await _ticketService.CreateAsync(createDto);
        if (ticket is null)
            return BadRequest("Рейс или пассажир не найдены");

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    /// <summary>
    /// Обновить данные билета.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TicketDto>> Update(int id, TicketCreateUpdateDto updateDto)
    {
        var ticket = await _ticketService.UpdateAsync(id, updateDto);
        if (ticket is null)
            return NotFound();

        return Ok(ticket);
    }

    /// <summary>
    /// Удалить билет.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _ticketService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
