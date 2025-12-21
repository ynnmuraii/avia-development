namespace Airline.Application.Contracts.AircraftFamilies;

/// <summary>
/// DTO для отображения информации о семействе самолётов.
/// </summary>
/// <param name="Id">Уникальный идентификатор семейства.</param>
/// <param name="Manufacturer">Производитель самолётов.</param>
/// <param name="FamilyName">Название семейства самолётов.</param>
public record AircraftFamilyDto(
    int Id,
    string Manufacturer,
    string FamilyName
);
