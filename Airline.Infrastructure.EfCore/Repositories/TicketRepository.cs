using Airline.Domain;
using Airline.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airline.Infrastructure.EfCore.Repositories;

/// <summary>
/// Специализированный репозиторий для сущности Ticket с явной загрузкой связанных данных.
/// Наследуется от EfCoreRepository<Ticket> и переопределяет методы для включения
/// связанных сущностей Flight и Passenger через .Include().
/// </summary>
public class TicketRepository(AirlineDbContext context) : EfCoreRepository<Ticket>(context)
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
}
