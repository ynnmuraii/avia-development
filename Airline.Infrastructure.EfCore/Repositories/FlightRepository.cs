using Airline.Domain;
using Airline.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airline.Infrastructure.EfCore.Repositories;

/// <summary>
/// Специализированный репозиторий для сущности Flight с явной загрузкой связанных данных.
/// Наследуется от EfCoreRepository<Flight> и переопределяет методы для включения
/// связанной сущности AircraftModel через .Include().
/// </summary>
public class FlightRepository(AirlineDbContext context) : EfCoreRepository<Flight>(context)
{
    /// <summary>
    /// Получить все рейсы с предварительно загруженными моделями самолетов.
    /// </summary>
    public override async Task<IEnumerable<Flight>> ReadAsync()
    {
        return await Context.Set<Flight>()
            .AsNoTracking()
            .Include(f => f.Model)
            .Include(f => f.Tickets)
            .ToListAsync();
    }

    /// <summary>
    /// Получить рейс по ID с предварительно загруженной моделью самолета и билетами.
    /// </summary>
    public override async Task<Flight?> ReadByIdAsync(int id)
    {
        return await Context.Set<Flight>()
            .Include(f => f.Model)
            .Include(f => f.Tickets)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}
