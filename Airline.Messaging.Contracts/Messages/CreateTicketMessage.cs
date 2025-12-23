namespace Airline.Messaging.Contracts.Messages;

/// <summary>
/// Сообщение для создания билета.
/// </summary>
/// <param name="FlightId">Идентификатор рейса.</param>
/// <param name="PassengerId">Идентификатор пассажира.</param>
/// <param name="SeatId">Номер посадочного места.</param>
/// <param name="HasCarryOn">Наличие ручной клади.</param>
/// <param name="BaggageKg">Вес багажа в килограммах.</param>
public record CreateTicketMessage(
    int FlightId,
    int PassengerId,
    string SeatId,
    bool? HasCarryOn,
    double BaggageKg
);
