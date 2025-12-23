namespace Airline.Application.Contracts.Tickets;

/// <summary>
/// DTO для отображения информации о билете.
/// </summary>
/// <param name="Id">Уникальный идентификатор билета.</param>
/// <param name="FlightId">Идентификатор рейса.</param>
/// <param name="PassengerId">Идентификатор пассажира.</param>
/// <param name="SeatId">Номер посадочного места.</param>
/// <param name="HasCarryOn">Наличие ручной клади.</param>
/// <param name="BaggageKg">Вес багажа в килограммах.</param>
public record TicketDto(
    int Id,
    int FlightId,
    int PassengerId,
    string SeatId,
    bool? HasCarryOn,
    double BaggageKg
);
