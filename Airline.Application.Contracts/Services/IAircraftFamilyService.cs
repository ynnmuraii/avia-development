using Airline.Application.Contracts.AircraftFamilies;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления семействами самолётов.
/// </summary>
public interface IAircraftFamilyService : IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>
{
}
