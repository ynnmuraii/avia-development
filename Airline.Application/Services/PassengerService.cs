using AutoMapper;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления пассажирами.
/// </summary>
public class PassengerService(
    IRepository<Passenger> repository,
    IMapper mapper) : IApplicationService<PassengerDto, PassengerCreateUpdateDto>
{
    /// <summary>
    /// Получить всех пассажиров.
    /// </summary>
    public async Task<IEnumerable<PassengerDto>> GetAllAsync()
    {
        var passengers = await repository.ReadAsync();
        return mapper.Map<IEnumerable<PassengerDto>>(passengers);
    }

    /// <summary>
    /// Получить пассажира по идентификатору.
    /// </summary>
    public async Task<PassengerDto?> GetByIdAsync(int id)
    {
        var passenger = await repository.ReadByIdAsync(id);
        return passenger is not null ? mapper.Map<PassengerDto>(passenger) : null;
    }

    /// <summary>
    /// Создать нового пассажира.
    /// </summary>
    public async Task<PassengerDto> CreateAsync(PassengerCreateUpdateDto createDto)
    {
        var passenger = mapper.Map<Passenger>(createDto);
        passenger.Id = 0;
        var created = await repository.CreateAsync(passenger);
        return mapper.Map<PassengerDto>(created);
    }

    /// <summary>
    /// Обновить данные пассажира.
    /// </summary>
    public async Task UpdateAsync(int id, PassengerCreateUpdateDto updateDto)
    {
        var passenger = mapper.Map<Passenger>(updateDto);
        passenger.Id = id;
        await repository.UpdateAsync(id, passenger);
    }

    /// <summary>
    /// Удалить пассажира.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
