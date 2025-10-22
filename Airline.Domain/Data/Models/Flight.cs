namespace Airline.Domain;

/// <summary>
/// Класс, представляющий рейс с информацией о маршруте, времени отправления/прибытия и используемой модели самолета.
/// </summary>
public class Flight
{
    public required string Code { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required DateOnly DateOfDeparture { get; set; }
    public required DateOnly DateOfArrival { get; set; }
    public required TimeOnly TimeOfDeparture { get; set; }
    public required TimeSpan FlightDuration { get; set; }
    public required AircraftModel Model { get; set; }
}
