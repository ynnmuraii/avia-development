using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления моделями самолётов.
/// </summary>
public class AircraftModelsController(IAircraftModelService service, ILogger<AircraftModelsController> logger) : CrudControllerBase<AircraftModelDto, AircraftModelCreateUpdateDto>(service, logger)
{
}

