namespace Airline.Messaging.Contracts.Messages;

/// <summary>
/// Сообщение для создания билета.
/// </summary>
public record CreateTicketMessage(
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
