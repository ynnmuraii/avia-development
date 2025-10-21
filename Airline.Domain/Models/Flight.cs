namespace Airline.Domain;

public class Flight
{
    public required string FlightCode {  get; set; }
    public required string PointOfDeparture { get; set; }
    public required string PointOfArrival { get; set; }
    public required DateOnly DateOfDeparture { get; set; }
    public required DateOnly DateofArrival { get; set; }
    public required TimeOnly TimeOfDeparture { get; set; }
    public required TimeSpan FlightDuration { get; set; }
    public required AircraftModel Model { get; set; }
}
