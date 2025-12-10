using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления рейсами.
/// </summary>
public class FlightsController : CrudControllerBase<FlightDto, FlightCreateUpdateDto, int>
{
    /// <summary>
    /// Инициализирует контроллер рейсов.
    /// </summary>
    public FlightsController(IFlightService service, ILogger<FlightsController> logger)
        : base(service, logger)
    {
    }
}
