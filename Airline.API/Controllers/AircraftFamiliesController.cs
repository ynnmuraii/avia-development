using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления семействами самолётов.
/// </summary>
public class AircraftFamiliesController : CrudControllerBase<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>
{
    /// <summary>
    /// Инициализирует контроллер семейств.
    /// </summary>
    public AircraftFamiliesController(IAircraftFamilyService service, ILogger<AircraftFamiliesController> logger)
        : base(service, logger)
    {
    }
}
