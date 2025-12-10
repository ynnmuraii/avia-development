using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления моделями самолётов.
/// </summary>
public class AircraftModelsController : CrudControllerBase<AircraftModelDto, AircraftModelCreateUpdateDto, int>
{
    /// <summary>
    /// Инициализирует контроллер моделей.
    /// </summary>
    public AircraftModelsController(IAircraftModelService service, ILogger<AircraftModelsController> logger)
        : base(service, logger)
    {
    }
}
