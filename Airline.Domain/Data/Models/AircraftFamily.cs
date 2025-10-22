namespace Airline.Domain;

/// <summary>
/// Класс, представляющий семейство самолетов с указанием производителя и названия семейства.
/// </summary>
public class AircraftFamily
{
    public required string Manufacturer { get; set; }
    public required string FamilyName { get; set; }
}
