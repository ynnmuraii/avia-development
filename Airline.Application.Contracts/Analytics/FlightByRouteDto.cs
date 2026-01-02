namespace Airline.Application.Contracts.Analytics;

/// <summary>
/// DTO для рейса по маршруту.
/// </summary>
public class FlightByRouteDto
{
    /// <summary>
    /// Идентификатор рейса.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Код рейса.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Пункт отправления.
    /// </summary>
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Пункт прибытия.
    /// </summary>
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Дата вылета.
    /// </summary>
    public DateOnly DateOfDeparture { get; set; }

    /// <summary>
    /// Время вылета.
    /// </summary>
    public TimeOnly TimeOfDeparture { get; set; }
}
