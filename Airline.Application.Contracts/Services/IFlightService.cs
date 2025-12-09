using Airline.Application.Contracts.Flights;

namespace Airline.Application.Contracts.Services;

/// <summary>
/// Интерфейс для сервиса управления рейсами.
/// </summary>
public interface IFlightService
{
    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    /// <returns>Список всех рейсов.</returns>
    Task<IEnumerable<FlightDto>> GetAllFlightsAsync();

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <returns>DTO рейса или null, если не найден.</returns>
    Task<FlightDto?> GetFlightByIdAsync(int id);

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    /// <param name="createDto">DTO для создания рейса.</param>
    /// <returns>Созданный рейс.</returns>
    Task<FlightDto> CreateFlightAsync(FlightCreateUpdateDto createDto);

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <param name="updateDto">DTO для обновления рейса.</param>
    /// <returns>Обновлённый рейс.</returns>
    Task<FlightDto?> UpdateFlightAsync(int id, FlightCreateUpdateDto updateDto);

    /// <summary>
    /// Удалить рейс.
    /// </summary>
    /// <param name="id">Идентификатор рейса.</param>
    /// <returns>true если удалён, false если не найден.</returns>
    Task<bool> DeleteFlightAsync(int id);
}
