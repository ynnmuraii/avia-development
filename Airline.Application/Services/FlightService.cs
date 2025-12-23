using AutoMapper;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления рейсами.
/// </summary>
public class FlightService(
    IRepository<Flight> repository,
    IRepository<AircraftModel> modelRepository,
    IMapper mapper) : IApplicationService<FlightDto, FlightCreateUpdateDto>
{
    /// <summary>
    /// Получить все рейсы.
    /// </summary>
    public async Task<IEnumerable<FlightDto>> GetAllAsync()
    {
        var flights = await repository.ReadAsync();
        return mapper.Map<IEnumerable<FlightDto>>(flights);
    }

    /// <summary>
    /// Получить рейс по идентификатору.
    /// </summary>
    public async Task<FlightDto?> GetByIdAsync(int id)
    {
        var flight = await repository.ReadByIdAsync(id);
        return flight is not null ? mapper.Map<FlightDto>(flight) : null;
    }

    /// <summary>
    /// Создать новый рейс.
    /// </summary>
    public async Task<FlightDto> CreateAsync(FlightCreateUpdateDto createDto)
    {

        var model = await modelRepository.ReadByIdAsync(createDto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"Aircraft model with id {createDto.ModelId} not found");


        var flight = mapper.Map<Flight>(createDto);
        flight.Model = model;

        var created = await repository.CreateAsync(flight);
        return mapper.Map<FlightDto>(created);
    }

    /// <summary>
    /// Обновить существующий рейс.
    /// </summary>
    public async Task UpdateAsync(int id, FlightCreateUpdateDto updateDto)
    {

        var existingFlight = await repository.ReadByIdAsync(id);
        if (existingFlight is null)
            throw new KeyNotFoundException($"Flight with id {id} not found");


        var model = await modelRepository.ReadByIdAsync(updateDto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"Aircraft model with id {updateDto.ModelId} not found");


        mapper.Map(updateDto, existingFlight);
        existingFlight.Model = model;


        await repository.UpdateAsync(id, existingFlight);
    }

    /// <summary>
    /// Удалить рейс.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var exists = await repository.ReadByIdAsync(id);
        if (exists is null)
            throw new KeyNotFoundException($"Flight with id {id} not found");

        await repository.DeleteAsync(id);
    }
}
