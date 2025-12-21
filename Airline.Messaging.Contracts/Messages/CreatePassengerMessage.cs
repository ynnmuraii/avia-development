namespace Airline.Messaging.Contracts.Messages;

/// <summary>
/// Сообщение для создания пассажира.
/// </summary>
public record CreatePassengerMessage
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
