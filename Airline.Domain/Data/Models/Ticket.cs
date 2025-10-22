namespace Airline.Domain;

/// <summary>
/// Класс, представляющий билет на рейс с информацией о пассажире, месте и багаже.
/// </summary>
public class Ticket
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Flight Flight { get; set; }
    public required Passenger Passenger { get; set; }
    public required string SeatId { get; set; }
    public required bool HasCarryOn { get; set; }
    public required int BaggageKg { get; set; }
}
