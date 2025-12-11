using AutoMapper;
using Airline.Application;
using Airline.Application.Contracts.Services;
using Airline.Application.Services;
using Airline.Domain;
using Airline.Domain.Repositories;
using Airline.Infrastructure.EfCore;
using Airline.Infrastructure.EfCore.Data;
using Airline.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// OpenTelemetry для отслеживания
builder.Services.AddOpenTelemetry()
    .WithTracing(t =>
    {
        t.AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation();
    });

// DbContext с MySQL
var connectionString = builder.Configuration.GetConnectionString("airline-db") 
    ?? "Server=localhost;Database=AirlineDb;User=root;Password=password";

builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

// Явная регистрация репозиториев для каждой сущности
builder.Services.AddScoped<IRepository<AircraftFamily>, EfCoreRepository<AircraftFamily>>();
builder.Services.AddScoped<IRepository<AircraftModel>, EfCoreRepository<AircraftModel>>();
builder.Services.AddScoped<IRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<IRepository<Passenger>, EfCoreRepository<Passenger>>();
builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();

builder.Services.AddScoped<IAircraftFamilyService, AircraftFamilyService>();
builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddAutoMapper(typeof(AirlineProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var assembly = Assembly.GetExecutingAssembly();
    
    // Подключаем XML самого API
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);

    // Подключаем XML всех зависимых проектов (Contracts, Domain и т.д.), которые загружены
    foreach (var refAssembly in assembly.GetReferencedAssemblies())
    {
        if (refAssembly.Name!.StartsWith("System") || refAssembly.Name.StartsWith("Microsoft"))
            continue;

        var refXmlPath = Path.Combine(AppContext.BaseDirectory, $"{refAssembly.Name}.xml");
        if (File.Exists(refXmlPath))
            options.IncludeXmlComments(refXmlPath);
    }
});

var app = builder.Build();

// Инициализация базы данных с миграциями и seed-данными
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AirlineDbContext>();
    try
    {
        await dbContext.InitializeAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", () => "Airline API Приложение");

app.Run();