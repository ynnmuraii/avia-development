namespace Airline.Application.Contracts.AircraftFamilies;

/// <summary>
/// DTO для отображения информации о семействе самолётов.
/// </summary>
public record AircraftFamilyDto(
    /// <summary>
    /// Уникальный идентификатор семейства.
    /// </summary>
    int Id,
    /// <summary>
    /// Производитель.
    /// </summary>
    string Manufacturer,
    /// <summary>
    /// Название семейства.
    /// </summary>
    string FamilyName
);
