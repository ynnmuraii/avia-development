using Microsoft.EntityFrameworkCore;
using Airline.Domain;

namespace Airline.Infrastructure.EfCore;

public class AirlineDbContext : DbContext
{
    public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
    {
    }

    public DbSet<AircraftFamily> AircraftFamilies { get; set; }
    public DbSet<AircraftModel> AircraftModels { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}