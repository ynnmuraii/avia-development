using Airline.Generator.Generators;
using Airline.Generator.Services;
using Airline.Generator.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<TicketGenerator>();

builder.Services.AddSingleton<RabbitMqPublisher>();

builder.Services.AddHostedService<DataGeneratorWorker>();

var host = builder.Build();
host.Run();
