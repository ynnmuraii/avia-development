namespace Airline.Application.Contracts.Passengers;

/// <summary>
/// DTO для отображения информации о пассажире.
/// </summary>
/// <param name="Id">Уникальный идентификатор пассажира.</param>
/// <param name="FirstName">Имя пассажира.</param>
/// <param name="LastName">Фамилия пассажира.</param>
/// <param name="Patronymic">Отчество пассажира.</param>
/// <param name="PassportNumber">Номер паспорта пассажира.</param>
/// <param name="BirthDate">Дата рождения пассажира.</param>
public record PassengerDto(
    int Id,
    string FirstName,
    string LastName,
    string? Patronymic,
    string PassportNumber,
    DateOnly? BirthDate
);
