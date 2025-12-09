using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления моделями самолётов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftModelsController : ControllerBase
{
    private readonly AircraftModelService _modelService;
    private readonly ILogger<AircraftModelsController> _logger;

    /// <summary>
    /// Инициализирует контроллер моделей.
    /// </summary>
    public AircraftModelsController(AircraftModelService modelService, ILogger<AircraftModelsController> logger)
    {
        _modelService = modelService;
        _logger = logger;
    }

    /// <summary>
    /// Получить все модели.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftModelDto>>> GetAll()
    {
        var models = await _modelService.GetAllAsync();
        return Ok(models);
    }

    /// <summary>
    /// Получить модель по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<AircraftModelDto>> GetById(int id)
    {
        var model = await _modelService.GetByIdAsync(id);
        if (model is null)
            return NotFound();

        return Ok(model);
    }

    /// <summary>
    /// Создать новую модель.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AircraftModelDto>> Create(AircraftModelCreateUpdateDto createDto)
    {
        var model = await _modelService.CreateAsync(createDto);
        if (model is null)
            return BadRequest("Семейство самолётов не найдено");

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    /// <summary>
    /// Обновить данные модели.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<AircraftModelDto>> Update(int id, AircraftModelCreateUpdateDto updateDto)
    {
        var model = await _modelService.UpdateAsync(id, updateDto);
        if (model is null)
            return NotFound();

        return Ok(model);
    }

    /// <summary>
    /// Удалить модель.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _modelService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
