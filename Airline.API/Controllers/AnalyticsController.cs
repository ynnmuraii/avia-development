using Microsoft.AspNetCore.Mvc;
using Airline.Application.Contracts.Services;

namespace Airline.API.Controllers;

/// <summary>
/// Контроллер для аналитики авиакомпании.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    /// <summary>
    /// Инициализирует контроллер аналитики.
    /// </summary>
    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    /// <summary>
    /// Получить рейсы с минимальной длительностью.
    /// </summary>
    /// <returns>Список рейсов с минимальной длительностью.</returns>
    [HttpGet("flights-with-minimal-duration")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetFlightsWithMinimalDuration()
    {
        var flights = await _analyticsService.GetFlightsWithMinimalDurationAsync();
        return Ok(flights);
    }

    /// <summary>
    /// Получить топ-5 рейсов по количеству пассажиров.
    /// </summary>
    /// <returns>Список из 5 рейсов.</returns>
    [HttpGet("top-5-flights-by-passengers")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetTop5FlightsByPassengers()
    {
        var flights = await _analyticsService.GetTop5FlightsByPassengerCountAsync();
        return Ok(flights);
    }

    /// <summary>
    /// Получить пассажиров без багажа для выбранного рейса.
    /// </summary>
    /// <param name="flightCode">Код рейса.</param>
    /// <returns>Список пассажиров без багажа.</returns>
    [HttpGet("passengers-without-baggage/{flightCode}")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetPassengersWithoutBaggage(string flightCode)
    {
        var passengers = await _analyticsService.GetPassengersWithoutBaggageAsync(flightCode);
        return Ok(passengers);
    }

    /// <summary>
    /// Получить рейсы выбранной модели за указанный период.
    /// </summary>
    /// <param name="modelId">Идентификатор модели.</param>
    /// <param name="startDate">Дата начала (формат: yyyy-MM-dd).</param>
    /// <param name="endDate">Дата конца (формат: yyyy-MM-dd).</param>
    /// <returns>Список рейсов.</returns>
    [HttpGet("flights-by-model-and-date")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetFlightsByModelAndDate(int modelId, string startDate, string endDate)
    {
        if (!DateOnly.TryParse(startDate, out var start) || !DateOnly.TryParse(endDate, out var end))
            return BadRequest("Invalid date format. Use yyyy-MM-dd");

        var flights = await _analyticsService.GetFlightsByModelAndDateAsync(modelId, start, end);
        return Ok(flights);
    }

    /// <summary>
    /// Получить рейсы по маршруту.
    /// </summary>
    /// <param name="from">Код аэропорта отправления.</param>
    /// <param name="to">Код аэропорта прибытия.</param>
    /// <returns>Список рейсов.</returns>
    [HttpGet("flights-by-route")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetFlightsByRoute(string from, string to)
    {
        var flights = await _analyticsService.GetFlightsByRouteAsync(from, to);
        return Ok(flights);
    }
}
