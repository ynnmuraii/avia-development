using AutoMapper;
using Airline.Application;
using Airline.Application.Contracts.Services;
using Airline.Application.Services;
using Airline.Domain.Repositories;
using Airline.Infrastructure.EfCore;
using Airline.Infrastructure.EfCore.Data;
using Airline.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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

// Сервисы приложения
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

builder.Services.AddScoped<IAircraftFamilyService, AircraftFamilyService>();
builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddAutoMapper(typeof(AirlineProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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