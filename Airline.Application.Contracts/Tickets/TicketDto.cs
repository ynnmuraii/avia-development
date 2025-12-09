namespace Airline.Application.Contracts.Tickets;

/// <summary>
/// DTO для отображения информации о билете.
/// </summary>
public record TicketDto(
    /// <summary>
    /// Уникальный идентификатор билета.
    /// </summary>
    int Id,
    /// <summary>
    /// Идентификатор рейса.
    /// </summary>
    int FlightId,
    /// <summary>
    /// Идентификатор пассажира.
    /// </summary>
    int PassengerId,
    /// <summary>
    /// Номер посадочного места.
    /// </summary>
    string SeatId,
    /// <summary>
    /// Наличие ручной клади.
    /// </summary>
    bool? HasCarryOn,
    /// <summary>
    /// Вес багажа в килограммах.
    /// </summary>
    double BaggageKg
);
