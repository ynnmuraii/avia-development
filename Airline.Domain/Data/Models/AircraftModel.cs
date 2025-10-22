namespace Airline.Domain;

/// <summary>
/// Класс, представляющий модель самолета с характеристиками дальности полета, количества мест и принадлежности к семейству.
/// </summary>
public class AircraftModel
{
    public required string ModelName { get; set;  }
    public required int RangeKm { get; set; }
    public required int Seats { get; set; }
    public required AircraftFamily Family { get; set; }
}
