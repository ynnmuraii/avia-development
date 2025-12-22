using AutoMapper;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления рейсами.
/// </summary>
public class FlightService : IApplicationService<FlightDto, FlightCreateUpdateDto>
{
    private readonly IRepository<Flight> _repository;
    private readonly IRepository<AircraftModel> _modelRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис рейсов.
    /// </summary>
    public FlightService(IRepository<Flight> repository, IRepository<AircraftModel> modelRepository, IMapper mapper)
    {
        _repository = repository;
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    public async Task<IEnumerable<FlightDto>> GetAllAsync()
    {
        var flights = await _repository.ReadAsync();
        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    public async Task<FlightDto?> GetByIdAsync(int id)
    {
        var flight = await _repository.ReadByIdAsync(id);
        return flight is not null ? _mapper.Map<FlightDto>(flight) : null;
    }

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    public async Task<FlightDto> CreateAsync(FlightCreateUpdateDto createDto)
    {

        var model = await _modelRepository.ReadByIdAsync(createDto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"Aircraft model with id {createDto.ModelId} not found");


        var flight = _mapper.Map<Flight>(createDto);
        flight.Model = model;

        var created = await _repository.CreateAsync(flight);
        return _mapper.Map<FlightDto>(created);
    }

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    public async Task UpdateAsync(int id, FlightCreateUpdateDto updateDto)
    {

        var existingFlight = await _repository.ReadByIdAsync(id);
        if (existingFlight is null)
            throw new KeyNotFoundException($"Flight with id {id} not found");


        var model = await _modelRepository.ReadByIdAsync(updateDto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"Aircraft model with id {updateDto.ModelId} not found");


        _mapper.Map(updateDto, existingFlight);
        existingFlight.Model = model;


        await _repository.UpdateAsync(id, existingFlight);
    }

    /// <summary>
    /// Удалить рейс.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var exists = await _repository.ReadByIdAsync(id);
        if (exists is null)
            throw new KeyNotFoundException($"Flight with id {id} not found");

        await _repository.DeleteAsync(id);
    }
}
