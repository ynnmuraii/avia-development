using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления семействами самолётов.
/// </summary>
public class AircraftFamiliesController(IAircraftFamilyService service, ILogger<AircraftFamiliesController> logger) : CrudControllerBase<AircraftFamilyDto, AircraftFamilyCreateUpdateDto>(service, logger)
{
}

