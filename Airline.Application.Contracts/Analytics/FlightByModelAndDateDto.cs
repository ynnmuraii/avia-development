namespace Airline.Application.Contracts.Analytics;

/// <summary>
/// DTO для рейса с моделью и датой.
/// </summary>
public record FlightByModelAndDateDto
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
    /// Название модели самолёта.
    /// </summary>
    public string ModelName { get; set; } = string.Empty;
}
