namespace Airline.Application.Contracts.Services;

/// <summary>
/// Базовый интерфейс для всех сервисов приложения с методами CRUD.
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Получить все сущности.
    /// </summary>
    public Task<IEnumerable<TDto>> GetAllAsync();

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public Task<TDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую сущность.
    /// </summary>
    public Task<TDto> CreateAsync(TCreateUpdateDto createDto);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    public Task UpdateAsync(int id, TCreateUpdateDto updateDto);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    public Task DeleteAsync(int id);
}
