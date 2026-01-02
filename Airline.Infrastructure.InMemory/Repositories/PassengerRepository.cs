using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для управления пассажирами в памяти.
/// </summary>
public class PassengerRepository : IRepository<Passenger>
{
    private readonly List<Passenger> _passengers;

    /// <summary>
    /// Инициализирует репозиторий с начальными данными.
    /// </summary>
    /// <param name="passengers">Начальный список пассажиров.</param>
    public PassengerRepository(List<Passenger> passengers)
    {
        _passengers = passengers;
    }

    /// <summary>
    /// Получить всех пассажиров.
    /// </summary>
    public Task<IEnumerable<Passenger>> ReadAsync() => Task.FromResult(_passengers.AsEnumerable());

    /// <summary>
    /// Получить пассажира по идентификатору.
    /// </summary>
    public Task<Passenger?> ReadByIdAsync(int id) => Task.FromResult(_passengers.FirstOrDefault(p => p.Id == id));

    /// <summary>
    /// Создать нового пассажира.
    /// </summary>
    public Task<Passenger> CreateAsync(Passenger entity)
    {
        entity.Id = _passengers.Count > 0 ? _passengers.Max(p => p.Id) + 1 : 1;
        _passengers.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить данные пассажира.
    /// </summary>
    public Task<Passenger?> UpdateAsync(int id, Passenger entity)
    {
        var passenger = _passengers.FirstOrDefault(p => p.Id == id);
        if (passenger is null)
            return Task.FromResult<Passenger?>(null);

        passenger.FirstName = entity.FirstName;
        passenger.LastName = entity.LastName;
        passenger.Patronymic = entity.Patronymic;
        passenger.PassportNumber = entity.PassportNumber;
        passenger.BirthDate = entity.BirthDate;

        return Task.FromResult<Passenger?>(passenger);
    }

    /// <summary>
    /// Удалить пассажира по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var passenger = _passengers.FirstOrDefault(p => p.Id == id);
        if (passenger is null)
            return Task.FromResult(false);

        _passengers.Remove(passenger);
        return Task.FromResult(true);
    }
}