using Airline.Domain;
using Airline.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airline.Infrastructure.EfCore.Repositories;

/// <summary>
/// Специализированный репозиторий для сущности Ticket с явной загрузкой связанных данных.
/// Наследуется от EfCoreRepository и реализует ITicketRepository.
/// </summary>
public class TicketRepository(AirlineDbContext context) : EfCoreRepository<Ticket>(context), ITicketRepository
{
    /// <summary>
    /// Получить все билеты с предварительно загруженными рейсами и пассажирами.
    /// </summary>
    public override async Task<IEnumerable<Ticket>> ReadAsync()
    {
        return await Context.Set<Ticket>()
            .AsNoTracking()
            .Include(t => t.Flight)
                .ThenInclude(f => f.Model)
            .Include(t => t.Passenger)
            .ToListAsync();
    }

    /// <summary>
    /// Получить билет по ID с предварительно загруженными рейсом и пассажиром.
    /// </summary>
    public override async Task<Ticket?> ReadByIdAsync(int id)
    {
        return await Context.Set<Ticket>()
            .Include(t => t.Flight)
                .ThenInclude(f => f.Model)
            .Include(t => t.Passenger)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Получить все билеты для указанного рейса.
    /// </summary>
    /// <param name="flightId">Идентификатор рейса.</param>
    /// <returns>Список билетов, принадлежащих указанному рейсу.</returns>
    public async Task<IEnumerable<Ticket>> GetByFlightIdAsync(int flightId)
    {
        return await Context.Set<Ticket>()
            .AsNoTracking()
            .Where(t => t.Flight.Id == flightId)
            .Include(t => t.Flight)
                .ThenInclude(f => f.Model)
            .Include(t => t.Passenger)
            .ToListAsync();
    }
}
