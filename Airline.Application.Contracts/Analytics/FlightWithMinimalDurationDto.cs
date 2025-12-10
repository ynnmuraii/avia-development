namespace Airline.Application.Contracts.Analytics;

/// <summary>
/// DTO для рейса с минимальной длительностью.
/// </summary>
public class FlightWithMinimalDurationDto
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
    /// Длительность полёта в минутах.
    /// </summary>
    public int FlightDuration { get; set; }
}
