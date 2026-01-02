using Microsoft.EntityFrameworkCore;
using Airline.Domain;

namespace Airline.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных авиакомпании.
/// </summary>
public class AirlineDbContext : DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр контекста базы данных.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста.</param>
    public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Семейства самолётов.
    /// </summary>
    public DbSet<AircraftFamily> AircraftFamilies { get; set; }

    /// <summary>
    /// Модели самолётов.
    /// </summary>
    public DbSet<AircraftModel> AircraftModels { get; set; }

    /// <summary>
    /// Рейсы.
    /// </summary>
    public DbSet<Flight> Flights { get; set; }

    /// <summary>
    /// Пассажиры.
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Билеты.
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Настраивает модель данных при создании.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}