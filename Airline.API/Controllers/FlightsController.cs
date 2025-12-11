using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления рейсами.
/// </summary>
public class FlightsController(IFlightService service, ILogger<FlightsController> logger) : CrudControllerBase<FlightDto, FlightCreateUpdateDto>(service, logger)
{
}

