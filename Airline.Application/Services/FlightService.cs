using AutoMapper;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления рейсами.
/// </summary>
public class FlightService : IFlightService
{
    private readonly IRepository<Flight> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис рейсов.
    /// </summary>
    /// <param name="repository">Репозиторий рейсов.</param>
    /// <param name="mapper">AutoMapper для преобразования сущностей в DTOs.</param>
    public FlightService(IRepository<Flight> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _repository.ReadAsync();
        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        var flight = await _repository.ReadByIdAsync(id);
        return flight is not null ? _mapper.Map<FlightDto>(flight) : null;
    }

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    public async Task<FlightDto> CreateFlightAsync(FlightCreateUpdateDto createDto)
    {
        var flight = _mapper.Map<Flight>(createDto);
        var created = await _repository.CreateAsync(flight);
        return _mapper.Map<FlightDto>(created);
    }

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    public async Task<FlightDto?> UpdateFlightAsync(int id, FlightCreateUpdateDto updateDto)
    {
        var flight = _mapper.Map<Flight>(updateDto);
        var updated = await _repository.UpdateAsync(id, flight);
        return updated is not null ? _mapper.Map<FlightDto>(updated) : null;
    }

    /// <summary>
    /// Удалить рейс.
    /// </summary>
    public async Task<bool> DeleteFlightAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
