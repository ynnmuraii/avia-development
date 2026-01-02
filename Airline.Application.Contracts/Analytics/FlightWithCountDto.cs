namespace Airline.Application.Contracts.Analytics;

/// <summary>
/// DTO для рейса с количеством пассажиров.
/// </summary>
public class FlightWithCountDto
{
    /// <summary>
    /// Идентификатор рейса.
    /// </summary>
    public int FlightId { get; set; }

    /// <summary>
    /// Код рейса.
    /// </summary>
    public string FlightCode { get; set; } = string.Empty;

    /// <summary>
    /// Количество пассажиров на рейсе.
    /// </summary>
    public int PassengerCount { get; set; }
}
