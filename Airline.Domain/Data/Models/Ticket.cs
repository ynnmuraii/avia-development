namespace Airline.Domain;

/// <summary>
/// Класс, представляющий билет на рейс с информацией о пассажире, месте и багаже.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Уникальный идентификатор билета.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Рейс, на который оформлен билет.
    /// </summary>
    public required Flight Flight { get; set; }

    /// <summary>
    /// Пассажир, на которого оформлен билет
    /// </summary>
    public required Passenger Passenger { get; set; }

    /// <summary>
    /// Номер посадочного места.
    /// </summary>
    public required string SeatId { get; set; }

    /// <summary>
    /// Наличие ручной клади.
    /// </summary>
    public bool? HasCarryOn { get; set; }

    /// <summary>
    /// Вес багажа (в кг).
    /// </summary>
    public float BaggageKg { get; set; }
}
