namespace Airline.Application.Contracts.AircraftModels;

/// <summary>
/// DTO для создания или обновления информации о модели самолёта.
/// </summary>
public record AircraftModelCreateUpdateDto(
    /// <summary>
    /// Название модели.
    /// </summary>
    string ModelName,
    /// <summary>
    /// Дальность полёта в км.
    /// </summary>
    int RangeKm,
    /// <summary>
    /// Количество мест.
    /// </summary>
    int Seats,
    /// <summary>
    /// Грузоподъёмность в кг.
    /// </summary>
    int CargoCapacityKg,
    /// <summary>
    /// Идентификатор семейства самолётов.
    /// </summary>
    int FamilyId
);
