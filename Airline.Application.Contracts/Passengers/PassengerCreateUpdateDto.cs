namespace Airline.Application.Contracts.Passengers;

/// <summary>
/// DTO для создания или обновления информации о пассажире.
/// </summary>
public record PassengerCreateUpdateDto
{
    /// <summary>
    /// Имя пассажира.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия пассажира.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Отчество пассажира.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Номер паспорта пассажира.
    /// </summary>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Дата рождения пассажира.
    /// </summary>
    public DateOnly? BirthDate { get; init; }
}
