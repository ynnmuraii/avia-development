namespace Airline.Application.Contracts.Flights;

/// <summary>
/// DTO для отображения информации о рейсе.
/// </summary>
public record FlightDto(
    /// <summary>
    /// Уникальный идентификатор рейса.
    /// </summary>
    int Id,
    /// <summary>
    /// Код рейса.
    /// </summary>
    string Code,
    /// <summary>
    /// Сокращённый код аэропорта отправления.
    /// </summary>
    string From,
    /// <summary>
    /// Сокращённый код аэропорта прибытия.
    /// </summary>
    string To,
    /// <summary>
    /// Дата вылета.
    /// </summary>
    DateOnly? DateOfDeparture,
    /// <summary>
    /// Дата прибытия.
    /// </summary>
    DateOnly? DateOfArrival,
    /// <summary>
    /// Время вылета.
    /// </summary>
    TimeOnly? TimeOfDeparture,
    /// <summary>
    /// Продолжительность полёта.
    /// </summary>
    TimeSpan? FlightDuration,
    /// <summary>
    /// Идентификатор модели самолёта.
    /// </summary>
    int ModelId
);
