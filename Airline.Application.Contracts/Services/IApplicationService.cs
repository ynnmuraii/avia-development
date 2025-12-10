namespace Airline.Application.Contracts.Services;

/// <summary>
/// Базовый интерфейс для всех сервисов приложения с методами CRUD.
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Получить все сущности.
    /// </summary>
    public Task<IEnumerable<TDto>> GetAllAsync();

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public Task<TDto?> GetByIdAsync(TKey id);

    /// <summary>
    /// Создать новую сущность.
    /// </summary>
    public Task<TDto> CreateAsync(TCreateUpdateDto createDto);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    public Task<TDto?> UpdateAsync(TKey id, TCreateUpdateDto updateDto);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(TKey id);
}
