using Airline.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airline.Infrastructure.EfCore.Repositories;

/// <summary>
/// Универсальный репозиторий, реализующий IRepository<T> с EF Core для CRUD операций.
/// </summary>
public class EfCoreRepository<T>(AirlineDbContext context) : IRepository<T> where T : class
{
    protected readonly AirlineDbContext Context = context;

    /// <summary>
    /// Получить все сущности типа T из базы данных.
    /// </summary>
    public async Task<IEnumerable<T>> ReadAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Получить сущность по её ID.
    /// </summary>
    public async Task<T?> ReadByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Создать новую сущность и сохранить её в базе данных.
    /// </summary>
    public async Task<T> CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Обновить существующую сущность по ID.
    /// </summary>
    public async Task<T?> UpdateAsync(int id, T entity)
    {
        var existing = await Context.Set<T>().FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        Context.Entry(existing).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удалить сущность по ID.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await Context.Set<T>().FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        Context.Set<T>().Remove(existing);
        await Context.SaveChangesAsync();
        return true;
    }
}

