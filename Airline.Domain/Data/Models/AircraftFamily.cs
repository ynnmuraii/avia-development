namespace Airline.Domain;

/// <summary>
/// Класс, представляющий семейство самолетов с указанием производителя и названия семейства.
/// </summary>
public class AircraftFamily
{
    /// <summary>
    /// Уникальный идентификатор семейства.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Производитель самолётов семейства
    /// </summary>
    public required string Manufacturer { get; set; }

    /// <summary>
    /// Название семейства моделей.
    /// </summary>
    public required string FamilyName { get; set; }
}
