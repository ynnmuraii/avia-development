using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для управления пассажирами.
/// </summary>
public class PassengersController : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto>
{
    /// <summary>
    /// Инициализирует контроллер пассажиров.
    /// </summary>
    public PassengersController(IPassengerService service, ILogger<PassengersController> logger) : base(service, logger)
    {
    }
}
