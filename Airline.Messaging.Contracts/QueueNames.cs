namespace Airline.Messaging.Contracts;

/// <summary>
/// Константы для именования очередей RabbitMQ.
/// </summary>
public static class QueueNames
{
    public const string Passengers = "airline.passengers";
    public const string AircraftFamilies = "airline.aircraft-families";
    public const string AircraftModels = "airline.aircraft-models";
    public const string Flights = "airline.flights";
    public const string Tickets = "airline.tickets";
}
