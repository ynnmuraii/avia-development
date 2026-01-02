namespace Airline.Domain;

/// <summary>
/// Класс, представляющий модель самолета с характеристиками дальности полета, количества мест и принадлежности к семейству.
/// </summary>
public class AircraftModel
{
    /// <summary>
    /// Уникальный идентификатор модели.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название модели самолёта
    /// </summary>
    public required string ModelName { get; set; }

    /// <summary>
    /// Максимальная дальность полёта (в км).
    /// </summary>
    public double RangeKm { get; set; }

    /// <summary>
    /// Количество пассажирских мест.
    /// </summary>
    public int Seats { get; set; }

    /// <summary>
    /// Грузовместимость (в кг).
    /// </summary>
    public double CargoCapacityKg { get; set; }

    /// <summary>
    /// Семейство модели.
    /// </summary>
    public required AircraftFamily Family { get; set; }
}
