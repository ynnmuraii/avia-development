using Airline.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AirlineData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Airline API работает");

app.Run();
