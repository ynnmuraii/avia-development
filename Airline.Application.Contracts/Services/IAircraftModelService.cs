using Airline.Application.Contracts.AircraftModels;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления моделями самолётов.
/// </summary>
public interface IAircraftModelService : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, int>
{
}
