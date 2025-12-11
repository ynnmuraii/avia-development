using Airline.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    IApplicationService<TDto, TCreateUpdateDto> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Получить все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var result = await appService.GetAllAsync();
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
    public virtual async Task<ActionResult<TDto>> Get(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Get), GetType().Name, id);
        try
        {
            var result = await appService.GetByIdAsync(id);
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
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(Create), GetType().Name);
        try
        {
            var created = await appService.CreateAsync(dto);
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
    public virtual async Task<IActionResult> Update(int id, [FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Update), GetType().Name, id);
        try
        {
            await appService.UpdateAsync(id, dto);
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
    public virtual async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with id={id}", nameof(Delete), GetType().Name, id);
        try
        {
            await appService.DeleteAsync(id);
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
