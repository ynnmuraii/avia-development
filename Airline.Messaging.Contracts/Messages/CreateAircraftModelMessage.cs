namespace Airline.Messaging.Contracts.Messages;

/// <summary>
/// Сообщение для создания модели самолёта.
/// </summary>
public record CreateAircraftModelMessage(
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
