using Airline.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AirlineData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Airline API работает");

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

app.Run();