namespace Airline.Domain.Repositories;

/// <summary>
/// Общий интерфейс репозитория для CRUD операций.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить все записи.
    /// </summary>
    /// <returns>Список всех записей.</returns>
    public Task<IEnumerable<T>> ReadAsync();

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>Запись или null, если не найдена.</returns>
    public Task<T?> ReadByIdAsync(int id);

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    /// <param name="entity">Сущность для создания.</param>
    /// <returns>Созданная сущность.</returns>
    public Task<T> CreateAsync(T entity);

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="entity">Обновлённая сущность.</param>
    /// <returns>Обновлённая сущность или null, если не найдена.</returns>
    public Task<T?> UpdateAsync(int id, T entity);

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>true если удалена, false если не найдена.</returns>
    public Task<bool> DeleteAsync(int id);
}
