using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Базовый класс контроллера для CRUD операций.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey> : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    protected readonly IApplicationService<TDto, TCreateUpdateDto, TKey> _service;
    protected readonly ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> _logger;

    /// <summary>
    /// Инициализирует базовый контроллер.
    /// </summary>
    protected CrudControllerBase(
        IApplicationService<TDto, TCreateUpdateDto, TKey> service,
        ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Получить все сущности.
    /// </summary>
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        _logger.LogInformation("Getting all entities");
        var entities = await _service.GetAllAsync();
        return Ok(entities);
    }

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> Get(TKey id)
    {
        _logger.LogInformation("Getting entity with id: {Id}", id);
        var entity = await _service.GetByIdAsync(id);
        if (entity is null)
        {
            _logger.LogWarning("Entity with id {Id} not found", id);
            return NotFound();
        }

        return Ok(entity);
    }

    /// <summary>
    /// Создать новую сущностт.
    /// </summary>
    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Create(TCreateUpdateDto createDto)
    {
        _logger.LogInformation("Creating new entity");
        try
        {
            var entity = await _service.CreateAsync(createDto);
            _logger.LogInformation("Entity created successfully");
            return CreatedAtAction(nameof(Get), entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating entity");
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TDto>> Update(TKey id, TCreateUpdateDto updateDto)
    {
        _logger.LogInformation("Updating entity with id: {Id}", id);
        try
        {
            var entity = await _service.UpdateAsync(id, updateDto);
            if (entity is null)
            {
                _logger.LogWarning("Entity with id {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Entity with id {Id} updated successfully", id);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating entity with id {Id}", id);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> Delete(TKey id)
    {
        _logger.LogInformation("Deleting entity with id: {Id}", id);
        var result = await _service.DeleteAsync(id);
        if (!result)
        {
            _logger.LogWarning("Entity with id {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Entity with id {Id} deleted successfully", id);
        return NoContent();
    }
}
