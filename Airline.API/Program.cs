using AutoMapper;
using Airline.Application;
using Airline.Application.Contracts.Services;
using Airline.Application.Services;
using Airline.Domain;
using Airline.Domain.Repositories;
using Airline.Infrastructure.InMemory.Data;
using Airline.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Initialize data
var families = DataSeeder.GetAircraftFamilies();
var models = DataSeeder.GetAircraftModels(families);
var flights = DataSeeder.GetFlights(models);
var passengers = DataSeeder.GetPassengers();
var tickets = DataSeeder.GetTickets(flights, passengers);

// Register repositories
builder.Services.AddSingleton<IRepository<AircraftFamily>>(new AircraftFamilyRepository(families));
builder.Services.AddSingleton<IRepository<AircraftModel>>(new AircraftModelRepository(models));
builder.Services.AddSingleton<IRepository<Flight>>(new FlightRepository(flights));
builder.Services.AddSingleton<IRepository<Passenger>>(new PassengerRepository(passengers));
builder.Services.AddSingleton<IRepository<Ticket>>(new TicketRepository(tickets));

// Register services
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<PassengerService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<AircraftModelService>();
builder.Services.AddScoped<AircraftFamilyService>();

builder.Services.AddAutoMapper(typeof(AirlineProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", () => "Airline API Приложение");

app.Run();

