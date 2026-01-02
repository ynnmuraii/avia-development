namespace Airline.Domain.Repositories;

/// <summary>
/// Общий интерфейс репозитория для CRUD операций.
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить все записи.
    /// </summary>
    public Task<IEnumerable<T>> ReadAsync();

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    public Task<T?> ReadByIdAsync(int id);

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    public Task<T> CreateAsync(T entity);

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    public Task<T?> UpdateAsync(int id, T entity);

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
