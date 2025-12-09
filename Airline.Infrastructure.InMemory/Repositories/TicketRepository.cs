using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий для управления билетами в памяти.
/// </summary>
public class TicketRepository : IRepository<Ticket>
{
    private readonly List<Ticket> _tickets;

    /// <summary>
    /// Инициализирует репозиторий с начальными данными.
    /// </summary>
    /// <param name="tickets">Начальный список билетов.</param>
    public TicketRepository(List<Ticket> tickets)
    {
        _tickets = tickets;
    }

    /// <summary>
    /// Получить все билеты.
    /// </summary>
    public Task<IEnumerable<Ticket>> ReadAsync() => Task.FromResult(_tickets.AsEnumerable());

    /// <summary>
    /// Получить билет по идентификатору.
    /// </summary>
    public Task<Ticket?> ReadByIdAsync(int id) => Task.FromResult(_tickets.FirstOrDefault(t => t.Id == id));

    /// <summary>
    /// Создать новый билет.
    /// </summary>
    public Task<Ticket> CreateAsync(Ticket entity)
    {
        entity.Id = _tickets.Count > 0 ? _tickets.Max(t => t.Id) + 1 : 1;
        _tickets.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить данные билета.
    /// </summary>
    public Task<Ticket?> UpdateAsync(int id, Ticket entity)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            return Task.FromResult<Ticket?>(null);

        ticket.Flight = entity.Flight;
        ticket.Passenger = entity.Passenger;
        ticket.SeatId = entity.SeatId;
        ticket.HasCarryOn = entity.HasCarryOn;
        ticket.BaggageKg = entity.BaggageKg;

        return Task.FromResult<Ticket?>(ticket);
    }

    /// <summary>
    /// Удалить билет по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
            return Task.FromResult(false);

        _tickets.Remove(ticket);
        return Task.FromResult(true);
    }
}
