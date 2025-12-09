namespace Airline.Application.Contracts.Flights;

/// <summary>
/// DTO для создания или обновления информации о рейсе.
/// </summary>
public record FlightCreateUpdateDto(
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
