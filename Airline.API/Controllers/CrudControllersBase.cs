using Airline.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.API.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций.
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения.</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания/обновления.</typeparam>
/// <param name="appService">Сервис приложения.</param>
/// <param name="logger">Логгер.</param>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    IApplicationService<TDto, TCreateUpdateDto> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Защищенное свойство для доступа к сервису из дочерних классов.
    /// </summary>
    protected readonly IApplicationService<TDto, TCreateUpdateDto> Service = appService;

    /// <summary>
    /// Получить все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var result = await Service.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(GetAll), ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получить запись по ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Get), GetType().Name, id);
        try
        {
            var result = await Service.GetByIdAsync(id);
            if (result == null) return NotFound($"Entity with id={id} not found");
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(Get), ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(Create), GetType().Name);
        try
        {
            var created = await Service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = 0 }, created);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(Create), ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Update), GetType().Name, id);
        try
        {
            await Service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("KeyNotFoundException in {method}: {message}", nameof(Update), ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(Update), ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Удалить запись.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Delete), GetType().Name, id);
        try
        {
            await Service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("KeyNotFoundException in {method}: {message}", nameof(Delete), ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception in {method}: {@exception}", nameof(Delete), ex);
            return StatusCode(500, ex.Message);
        }
    }
}
