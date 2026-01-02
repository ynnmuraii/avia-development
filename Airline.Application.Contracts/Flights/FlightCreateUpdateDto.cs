namespace Airline.Application.Contracts.Flights;

/// <summary>
/// DTO для создания или обновления информации о рейсе.
/// </summary>
/// <param name="Code">Код рейса.</param>
/// <param name="From">Сокращённый код аэропорта отправления.</param>
/// <param name="To">Сокращённый код аэропорта прибытия.</param>
/// <param name="DateOfDeparture">Дата вылета.</param>
/// <param name="DateOfArrival">Дата прибытия.</param>
/// <param name="TimeOfDeparture">Время вылета.</param>
/// <param name="FlightDuration">Продолжительность полёта.</param>
/// <param name="ModelId">Идентификатор модели самолёта.</param>
public record FlightCreateUpdateDto(
    string Code,
    string From,
    string To,
    DateOnly? DateOfDeparture,
    DateOnly? DateOfArrival,
    TimeOnly? TimeOfDeparture,
    TimeSpan? FlightDuration,
    int ModelId
);
