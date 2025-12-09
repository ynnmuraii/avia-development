using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления семействами самолётов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftFamiliesController : ControllerBase
{
    private readonly AircraftFamilyService _familyService;
    private readonly ILogger<AircraftFamiliesController> _logger;

    /// <summary>
    /// Инициализирует контроллер семейств.
    /// </summary>
    public AircraftFamiliesController(AircraftFamilyService familyService, ILogger<AircraftFamiliesController> logger)
    {
        _familyService = familyService;
        _logger = logger;
    }

    /// <summary>
    /// Получить все семейства.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftFamilyDto>>> GetAll()
    {
        var families = await _familyService.GetAllAsync();
        return Ok(families);
    }

    /// <summary>
    /// Получить семейство по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<AircraftFamilyDto>> GetById(int id)
    {
        var family = await _familyService.GetByIdAsync(id);
        if (family is null)
            return NotFound();

        return Ok(family);
    }

    /// <summary>
    /// Создать новое семейство.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AircraftFamilyDto>> Create(AircraftFamilyCreateUpdateDto createDto)
    {
        var family = await _familyService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = family.Id }, family);
    }

    /// <summary>
    /// Обновить данные семейства.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<AircraftFamilyDto>> Update(int id, AircraftFamilyCreateUpdateDto updateDto)
    {
        var family = await _familyService.UpdateAsync(id, updateDto);
        if (family is null)
            return NotFound();

        return Ok(family);
    }

    /// <summary>
    /// Удалить семейство.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _familyService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
