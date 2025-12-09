using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления пассажирами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PassengersController : ControllerBase
{
    private readonly PassengerService _passengerService;
    private readonly ILogger<PassengersController> _logger;

    /// <summary>
    /// Инициализирует контроллер пассажиров.
    /// </summary>
    public PassengersController(PassengerService passengerService, ILogger<PassengersController> logger)
    {
        _passengerService = passengerService;
        _logger = logger;
    }

    /// <summary>
    /// Получить всех пассажиров.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerDto>>> GetAll()
    {
        var passengers = await _passengerService.GetAllAsync();
        return Ok(passengers);
    }

    /// <summary>
    /// Получить пассажира по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PassengerDto>> GetById(int id)
    {
        var passenger = await _passengerService.GetByIdAsync(id);
        if (passenger is null)
            return NotFound();

        return Ok(passenger);
    }

    /// <summary>
    /// Создать нового пассажира.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PassengerDto>> Create(PassengerCreateUpdateDto createDto)
    {
        var passenger = await _passengerService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = passenger.Id }, passenger);
    }

    /// <summary>
    /// Обновить данные пассажира.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PassengerDto>> Update(int id, PassengerCreateUpdateDto updateDto)
    {
        var passenger = await _passengerService.UpdateAsync(id, updateDto);
        if (passenger is null)
            return NotFound();

        return Ok(passenger);
    }

    /// <summary>
    /// Удалить пассажира.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _passengerService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
