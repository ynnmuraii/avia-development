using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для управления рейсами в памяти.
/// </summary>
public class FlightRepository : IRepository<Flight>
{
    private readonly List<Flight> _flights;

    /// <summary>
    /// Инициализирует репозиторий с начальными данными.
    /// </summary>
    /// <param name="flights">Начальный список рейсов.</param>
    public FlightRepository(List<Flight> flights)
    {
        _flights = flights;
    }

    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    public Task<IEnumerable<Flight>> ReadAsync() => Task.FromResult(_flights.AsEnumerable());

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    public Task<Flight?> ReadByIdAsync(int id) => Task.FromResult(_flights.FirstOrDefault(f => f.Id == id));

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    public Task<Flight> CreateAsync(Flight entity)
    {
        entity.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1;
        _flights.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    public Task<Flight?> UpdateAsync(int id, Flight entity)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == id);
        if (flight is null)
            return Task.FromResult<Flight?>(null);

        flight.Code = entity.Code;
        flight.From = entity.From;
        flight.To = entity.To;
        flight.DateOfDeparture = entity.DateOfDeparture;
        flight.DateOfArrival = entity.DateOfArrival;
        flight.TimeOfDeparture = entity.TimeOfDeparture;
        flight.FlightDuration = entity.FlightDuration;
        flight.Model = entity.Model;

        return Task.FromResult<Flight?>(flight);
    }

    /// <summary>
    /// Удалить рейс по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == id);
        if (flight is null)
            return Task.FromResult(false);

        _flights.Remove(flight);
        return Task.FromResult(true);
    }
}
