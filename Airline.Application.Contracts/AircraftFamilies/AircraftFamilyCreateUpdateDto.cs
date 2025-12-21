namespace Airline.Application.Contracts.AircraftFamilies;

/// <summary>
/// DTO для создания или обновления информации о семействе самолётов.
/// </summary>
/// <summary>
/// DTO для создания или обновления информации о семействе самолётов.
/// </summary>
/// <param name="Manufacturer">Производитель.</param>
/// <param name="FamilyName">Название семейства.</param>
public record AircraftFamilyCreateUpdateDto(
    string Manufacturer,
    string FamilyName
);
