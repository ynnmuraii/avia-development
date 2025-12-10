using Airline.Application.Contracts.Passengers;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления пассажирами.
/// </summary>
public interface IPassengerService : IApplicationService<PassengerDto, PassengerCreateUpdateDto, int>
{
}
