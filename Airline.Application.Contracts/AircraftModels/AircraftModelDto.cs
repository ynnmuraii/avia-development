namespace Airline.Application.Contracts.AircraftModels;

/// <summary>
/// DTO для отображения информации о модели самолёта.
/// </summary>
/// <param name="Id">Уникальный идентификатор модели.</param>
/// <param name="ModelName">Название модели.</param>
/// <param name="RangeKm">Дальность полёта в км.</param>
/// <param name="Seats">Количество мест.</param>
/// <param name="CargoCapacityKg">Грузоподъёмность в кг.</param>
/// <param name="FamilyId">Идентификатор семейства самолётов.</param>
public record AircraftModelDto(
    int Id,
    string ModelName,
    int RangeKm,
    int Seats,
    int CargoCapacityKg,
    int FamilyId
);
