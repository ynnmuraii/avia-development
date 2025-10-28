using Airline.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AirlineData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Airline API работает");

// ============================
// CRUD дл€ AircraftFamilies
// ============================
app.MapGet("/api/families", (AirlineData db) => db.Families);
app.MapGet("/api/families/{id:int}", (AirlineData db, int id) =>
{
    var fam = db.Families.FirstOrDefault(f => f.Id == id);
    return fam is not null ? Results.Ok(fam) : Results.NotFound();
});

app.MapPost("/api/families", (AirlineData db, AircraftFamily family) =>
{
    family.Id = db.Families.Max(f => f.Id) + 1;
    db.Families.Add(family);
    return Results.Created($"/api/families/{family.Id}", family);
});

app.MapPut("/api/families/{id:int}", (AirlineData db, int id, AircraftFamily input) =>
{
    var fam = db.Families.FirstOrDefault(f => f.Id == id);
    if (fam is null) return Results.NotFound();

    fam.Manufacturer = input.Manufacturer;
    fam.FamilyName = input.FamilyName;
    return Results.Ok(fam);
});

app.MapDelete("/api/families/{id:int}", (AirlineData db, int id) =>
{
    var fam = db.Families.FirstOrDefault(f => f.Id == id);
    if (fam is null) return Results.NotFound();

    db.Families.Remove(fam);
    return Results.NoContent();
});

// ============================
// CRUD дл€ AircraftModel
// ============================
app.MapGet("/api/models", (AirlineData db) => db.Models);

app.MapGet("/api/models/{id:int}", (AirlineData db, int id) =>
{
    var model = db.Models.FirstOrDefault(m => m.Id == id);
    return model is not null ? Results.Ok(model) : Results.NotFound();
});

app.MapPost("/api/models", (AirlineData db, AircraftModel model) =>
{
    var family = db.Families.FirstOrDefault(f => f.Id == model.Family.Id);
    if (family is null)
        return Results.BadRequest("”казанное семейство не найдено");

    model.Id = db.Models.Max(m => m.Id) + 1;
    model.Family = family;
    db.Models.Add(model);

    return Results.Created($"/api/models/{model.Id}", model);
});

app.MapPut("/api/models/{id:int}", (AirlineData db, int id, AircraftModel input) =>
{
    var model = db.Models.FirstOrDefault(m => m.Id == id);
    if (model is null) return Results.NotFound();

    var family = db.Families.FirstOrDefault(f => f.Id == input.Family.Id);
    if (family is null)
        return Results.BadRequest("”казанное семейство не найдено");

    model.ModelName = input.ModelName;
    model.RangeKm = input.RangeKm;
    model.Seats = input.Seats;
    model.CargoCapacityKg = input.CargoCapacityKg;
    model.Family = family;

    return Results.Ok(model);
});

app.MapDelete("/api/models/{id:int}", (AirlineData db, int id) =>
{
    var model = db.Models.FirstOrDefault(m => m.Id == id);
    if (model is null) return Results.NotFound();

    db.Models.Remove(model);
    return Results.NoContent();
});

// ============================
// CRUD дл€ Flight
// ============================
app.MapGet("/api/flights", (AirlineData db) => db.Flights);

app.MapGet("/api/flights/{id:int}", (AirlineData db, int id) =>
{
    var flight = db.Flights.FirstOrDefault(f => f.Id == id);
    return flight is not null ? Results.Ok(flight) : Results.NotFound();
});

app.MapPost("/api/flights", (AirlineData db, Flight flight) =>
{
    var model = db.Models.FirstOrDefault(m => m.Id == flight.Model.Id);
    if (model is null)
        return Results.BadRequest("”казанна€ модель самолЄта не найдена");

    flight.Id = db.Flights.Max(f => f.Id) + 1;
    flight.Model = model;
    db.Flights.Add(flight);

    return Results.Created($"/api/flights/{flight.Id}", flight);
});

app.MapPut("/api/flights/{id:int}", (AirlineData db, int id, Flight input) =>
{
    var flight = db.Flights.FirstOrDefault(f => f.Id == id);
    if (flight is null) return Results.NotFound();

    var model = db.Models.FirstOrDefault(m => m.Id == input.Model.Id);
    if (model is null)
        return Results.BadRequest("”казанна€ модель самолЄта не найдена");

    flight.Code = input.Code;
    flight.From = input.From;
    flight.To = input.To;
    flight.DateOfDeparture = input.DateOfDeparture;
    flight.DateOfArrival = input.DateOfArrival;
    flight.TimeOfDeparture = input.TimeOfDeparture;
    flight.FlightDuration = input.FlightDuration;
    flight.Model = model;

    return Results.Ok(flight);
});

app.MapDelete("/api/flights/{id:int}", (AirlineData db, int id) =>
{
    var flight = db.Flights.FirstOrDefault(f => f.Id == id);
    if (flight is null) return Results.NotFound();

    db.Flights.Remove(flight);
    return Results.NoContent();
});

// ============================
// CRUD дл€ Passenger
// ============================
app.MapGet("/api/passengers", (AirlineData db) => db.Passengers);

app.MapGet("/api/passengers/{id:int}", (AirlineData db, int id) =>
{
    var passenger = db.Passengers.FirstOrDefault(p => p.Id == id);
    return passenger is not null ? Results.Ok(passenger) : Results.NotFound();
});

app.MapPost("/api/passengers", (AirlineData db, Passenger p) =>
{
    p.Id = db.Passengers.Max(x => x.Id) + 1;
    db.Passengers.Add(p);
    return Results.Created($"/api/passengers/{p.Id}", p);
});

app.MapPut("/api/passengers/{id:int}", (AirlineData db, int id, Passenger input) =>
{
    var passenger = db.Passengers.FirstOrDefault(p => p.Id == id);
    if (passenger is null) return Results.NotFound();

    passenger.FirstName = input.FirstName;
    passenger.LastName = input.LastName;
    passenger.Patronymic = input.Patronymic;
    passenger.PassportNumber = input.PassportNumber;
    passenger.BirthDate = input.BirthDate;

    return Results.Ok(passenger);
});

app.MapDelete("/api/passengers/{id:int}", (AirlineData db, int id) =>
{
    var passenger = db.Passengers.FirstOrDefault(p => p.Id == id);
    if (passenger is null) return Results.NotFound();

    db.Passengers.Remove(passenger);
    return Results.NoContent();
});

// ============================
// CRUD дл€ Ticket
// ============================
app.MapGet("/api/tickets", (AirlineData db) => db.Tickets);

app.MapGet("/api/tickets/{id:int}", (AirlineData db, int id) =>
{
    var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
    return ticket is not null ? Results.Ok(ticket) : Results.NotFound();
});

app.MapPost("/api/tickets", (AirlineData db, Ticket ticket) =>
{
    var passenger = db.Passengers.FirstOrDefault(p => p.Id == ticket.Passenger.Id);
    if (passenger is null)
        return Results.BadRequest("ѕассажир с указанным Id не найден");

    var flight = db.Flights.FirstOrDefault(f => f.Id == ticket.Flight.Id);
    if (flight is null)
        return Results.BadRequest("–ейс с указанным Id не найден");

    ticket.Id = db.Tickets.Max(t => t.Id) + 1;
    ticket.Passenger = passenger;
    ticket.Flight = flight;

    db.Tickets.Add(ticket);
    return Results.Created($"/api/tickets/{ticket.Id}", ticket);
});

app.MapPut("/api/tickets/{id:int}", (AirlineData db, int id, Ticket input) =>
{
    var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
    if (ticket is null) return Results.NotFound();

    var passenger = db.Passengers.FirstOrDefault(p => p.Id == input.Passenger.Id);
    if (passenger is null)
        return Results.BadRequest("ѕассажир с указанным Id не найден");

    var flight = db.Flights.FirstOrDefault(f => f.Id == input.Flight.Id);
    if (flight is null)
        return Results.BadRequest("–ейс с указанным Id не найден");

    ticket.Passenger = passenger;
    ticket.Flight = flight;
    ticket.SeatId = input.SeatId;
    ticket.HasCarryOn = input.HasCarryOn;
    ticket.BaggageKg = input.BaggageKg;

    return Results.Ok(ticket);
});

app.MapDelete("/api/tickets/{id:int}", (AirlineData db, int id) =>
{
    var ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
    if (ticket is null) return Results.NotFound();

    db.Tickets.Remove(ticket);
    return Results.NoContent();
});

app.Run();