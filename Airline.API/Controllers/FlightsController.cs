using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления рейсами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly ILogger<FlightsController> _logger;

    /// <summary>
    /// Инициализирует контроллер рейсов.
    /// </summary>
    public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
    {
        _flightService = flightService;
        _logger = logger;
    }

    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    /// <returns>Список всех рейсов.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightDto>>> GetAll()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        return Ok(flights);
    }

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <returns>DTO рейса.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<FlightDto>> GetById(int id)
    {
        var flight = await _flightService.GetFlightByIdAsync(id);
        if (flight is null)
            return NotFound();

        return Ok(flight);
    }

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    /// <param name="createDto">DTO для создания рейса.</param>
    /// <returns>Созданный рейс.</returns>
    [HttpPost]
    public async Task<ActionResult<FlightDto>> Create(FlightCreateUpdateDto createDto)
    {
        var flight = await _flightService.CreateFlightAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = flight.Id }, flight);
    }

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <param name="updateDto">DTO для обновления.</param>
    /// <returns>Обновлённый рейс.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<FlightDto>> Update(int id, FlightCreateUpdateDto updateDto)
    {
        var flight = await _flightService.UpdateFlightAsync(id, updateDto);
        if (flight is null)
            return NotFound();

        return Ok(flight);
    }

    /// <summary>
    /// Удалить рейс.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _flightService.DeleteFlightAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
