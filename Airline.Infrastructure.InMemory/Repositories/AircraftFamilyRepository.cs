using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для управления семействами самолётов в памяти.
/// </summary>
public class AircraftFamilyRepository : IRepository<AircraftFamily>
{
    private readonly List<AircraftFamily> _families;

    /// <summary>
    /// Инициализирует репозиторий с начальными данными.
    /// </summary>
    /// <param name="families">Начальный список семейств.</param>
    public AircraftFamilyRepository(List<AircraftFamily> families)
    {
        _families = families;
    }

    /// <summary>
    /// Получить все семейства.
    /// </summary>
    public Task<IEnumerable<AircraftFamily>> ReadAsync() => Task.FromResult(_families.AsEnumerable());

    /// <summary>
    /// Получить семейство по идентификатору.
    /// </summary>
    public Task<AircraftFamily?> ReadByIdAsync(int id) => Task.FromResult(_families.FirstOrDefault(f => f.Id == id));

    /// <summary>
    /// Создать новое семейство.
    /// </summary>
    public Task<AircraftFamily> CreateAsync(AircraftFamily entity)
    {
        entity.Id = _families.Count > 0 ? _families.Max(f => f.Id) + 1 : 1;
        _families.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить данные семейства.
    /// </summary>
    public Task<AircraftFamily?> UpdateAsync(int id, AircraftFamily entity)
    {
        var family = _families.FirstOrDefault(f => f.Id == id);
        if (family is null)
            return Task.FromResult<AircraftFamily?>(null);

        family.Manufacturer = entity.Manufacturer;
        family.FamilyName = entity.FamilyName;

        return Task.FromResult<AircraftFamily?>(family);
    }

    /// <summary>
    /// Удалить семейство по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var family = _families.FirstOrDefault(f => f.Id == id);
        if (family is null)
            return Task.FromResult(false);

        _families.Remove(family);
        return Task.FromResult(true);
    }
}
