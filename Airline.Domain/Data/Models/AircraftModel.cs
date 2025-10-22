namespace Airline.Domain;

public class AircraftModel
{
    public required string ModelName { get; set;  }
    public required int RangeKm { get; set; }
    public required int Seats { get; set; }
    public required AircraftFamily Family { get; set; }
}
