namespace Airline.Domain;

/// <summary>
/// Класс, представляющий рейс с информацией о маршруте, времени отправления/прибытия и используемой модели самолета.
/// </summary>
public class Flight
{
    /// <summary>
    /// Уникальный идентификатор рейса.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Код рейса,
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Сокращённый код аэропорта отправления.
    /// </summary>
    public required string From { get; set; }

    /// <summary>
    /// Сокращённый код аэропорта прибытия.
    /// </summary>
    public required string To { get; set; }

    /// <summary>
    /// Дата вылета.
    /// </summary>
    public DateOnly? DateOfDeparture { get; set; }

    /// <summary>
    /// Дата прибытия.
    /// </summary>
    public DateOnly? DateOfArrival { get; set; }

    /// <summary>
    /// Время вылета.
    /// </summary>
    public TimeOnly? TimeOfDeparture { get; set; }

    /// <summary>
    /// Продолжительность полёта.
    /// </summary>
    public TimeSpan? FlightDuration { get; set; }

    /// <summary>
    /// Модель самолёта, выполняющего рейс.
    /// </summary>
    public required AircraftModel Model { get; set; }
}



