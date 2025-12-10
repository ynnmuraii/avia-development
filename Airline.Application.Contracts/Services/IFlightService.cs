using Airline.Application.Contracts.Flights;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления рейсами.
/// </summary>
public interface IFlightService : IApplicationService<FlightDto, FlightCreateUpdateDto, int>
{
}
