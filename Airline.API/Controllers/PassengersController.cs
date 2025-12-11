using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления пассажирами.
/// </summary>
public class PassengersController(IPassengerService service, ILogger<PassengersController> logger) : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto>(service, logger)
{
}

