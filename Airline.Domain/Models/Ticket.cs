namespace Airline.Domain;

public class Ticket
{
    public required Flight Flight { get; set; }
    public required Passenger Passenger { get; set; }
    public required string SeatId { get; set; }
    public required bool HasCarryOn { get; set; }
    public required int BaggageKg { get; set; }
}
