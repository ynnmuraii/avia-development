namespace Airline.Application.Contracts.Tickets;

/// <summary>
/// DTO для создания или обновления информации о билете.
/// </summary>
public record TicketCreateUpdateDto(
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
