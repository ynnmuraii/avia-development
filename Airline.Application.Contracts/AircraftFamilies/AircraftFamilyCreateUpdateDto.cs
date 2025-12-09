namespace Airline.Application.Contracts.AircraftFamilies;

/// <summary>
/// DTO для создания или обновления информации о семействе самолётов.
/// </summary>
public record AircraftFamilyCreateUpdateDto(
    /// <summary>
    /// Производитель.
    /// </summary>
    string Manufacturer,
    /// <summary>
    /// Название семейства.
    /// </summary>
    string FamilyName
);
