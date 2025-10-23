namespace Airline.Domain;

/// <summary>
/// Класс, представляющий пассажира с персональными данными и паспортной информацией.
/// </summary>
public class Passenger
{
    /// <summary>
    /// Уникальный идентификатор пассажира.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя пассажира.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия пассажира.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество пассажира.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Номер паспорта пассажира.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Дата рождения пассажира.
    /// </summary>
    public DateOnly? BirthDate { get; set; }
}
