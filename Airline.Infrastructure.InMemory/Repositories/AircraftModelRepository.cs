using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для управления моделями самолётов в памяти.
/// </summary>
public class AircraftModelRepository : IRepository<AircraftModel>
{
    private readonly List<AircraftModel> _models;

    /// <summary>
    /// Инициализирует репозиторий с начальными данными.
    /// </summary>
    /// <param name="models">Начальный список моделей.</param>
    public AircraftModelRepository(List<AircraftModel> models)
    {
        _models = models;
    }

    /// <summary>
    /// Получить все модели.
    /// </summary>
    public Task<IEnumerable<AircraftModel>> ReadAsync() => Task.FromResult(_models.AsEnumerable());

    /// <summary>
    /// Получить модель по идентификатору.
    /// </summary>
    public Task<AircraftModel?> ReadByIdAsync(int id) => Task.FromResult(_models.FirstOrDefault(m => m.Id == id));

    /// <summary>
    /// Создать новую модель.
    /// </summary>
    public Task<AircraftModel> CreateAsync(AircraftModel entity)
    {
        entity.Id = _models.Count > 0 ? _models.Max(m => m.Id) + 1 : 1;
        _models.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить данные модели.
    /// </summary>
    public Task<AircraftModel?> UpdateAsync(int id, AircraftModel entity)
    {
        var model = _models.FirstOrDefault(m => m.Id == id);
        if (model is null)
            return Task.FromResult<AircraftModel?>(null);

        model.ModelName = entity.ModelName;
        model.RangeKm = entity.RangeKm;
        model.Seats = entity.Seats;
        model.CargoCapacityKg = entity.CargoCapacityKg;
        model.Family = entity.Family;

        return Task.FromResult<AircraftModel?>(model);
    }

    /// <summary>
    /// Удалить модель по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var model = _models.FirstOrDefault(m => m.Id == id);
        if (model is null)
            return Task.FromResult(false);

        _models.Remove(model);
        return Task.FromResult(true);
    }
}
