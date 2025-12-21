namespace Airline.Messaging.Contracts.Messages;

/// <summary>
/// Сообщение для создания семейства самолётов.
/// </summary>
public record CreateAircraftFamilyMessage(
    /// <summary>
    /// Производитель.
    /// </summary>
    string Manufacturer,
    /// <summary>
    /// Название семейства.
    /// </summary>
    string FamilyName
);
