using Airline.Generator.Generators;
using Airline.Generator.Services;
using Airline.Generator.Workers;

var builder = Host.CreateApplicationBuilder(args);

// Добавляем Aspire service defaults
builder.AddServiceDefaults();

// Регистрируем генераторы
builder.Services.AddSingleton<PassengerGenerator>();
builder.Services.AddSingleton<AircraftFamilyGenerator>();
builder.Services.AddSingleton<AircraftModelGenerator>();
builder.Services.AddSingleton<FlightGenerator>();
builder.Services.AddSingleton<TicketGenerator>();

// Регистрируем RabbitMQ publisher
builder.Services.AddSingleton<RabbitMqPublisher>();

// Регистрируем фоновую службу генерации данных
builder.Services.AddHostedService<DataGeneratorWorker>();

var host = builder.Build();
host.Run();
