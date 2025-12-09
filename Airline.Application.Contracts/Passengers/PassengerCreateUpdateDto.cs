namespace Airline.Application.Contracts.Passengers;

/// <summary>
/// DTO для создания или обновления информации о пассажире.
/// </summary>
public record PassengerCreateUpdateDto(
    /// <summary>
    /// Имя пассажира.
    /// </summary>
    string FirstName,
    /// <summary>
    /// Фамилия пассажира.
    /// </summary>
    string LastName,
    /// <summary>
    /// Отчество пассажира.
    /// </summary>
    string? Patronymic,
    /// <summary>
    /// Номер паспорта пассажира.
    /// </summary>
    string PassportNumber,
    /// <summary>
    /// Дата рождения пассажира.
    /// </summary>
    DateOnly? BirthDate
);
