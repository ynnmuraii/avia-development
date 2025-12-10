using AutoMapper;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления пассажирами.
/// </summary>
public class PassengerService : IPassengerService
{
    private readonly IRepository<Passenger> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис пассажиров.
    /// </summary>
    public PassengerService(IRepository<Passenger> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить всех пассажиров.
    /// </summary>
    public async Task<IEnumerable<PassengerDto>> GetAllAsync()
    {
        var passengers = await _repository.ReadAsync();
        return _mapper.Map<IEnumerable<PassengerDto>>(passengers);
    }

    /// <summary>
    /// Получить пассажира по идентификатору.
    /// </summary>
    public async Task<PassengerDto?> GetByIdAsync(int id)
    {
        var passenger = await _repository.ReadByIdAsync(id);
        return passenger is not null ? _mapper.Map<PassengerDto>(passenger) : null;
    }

    /// <summary>
    /// Создать нового пассажира.
    /// </summary>
    public async Task<PassengerDto> CreateAsync(PassengerCreateUpdateDto createDto)
    {
        var passenger = _mapper.Map<Passenger>(createDto);
        passenger.Id = 0;
        var created = await _repository.CreateAsync(passenger);
        return _mapper.Map<PassengerDto>(created);
    }

    /// <summary>
    /// Обновить данные пассажира.
    /// </summary>
    public async Task<PassengerDto?> UpdateAsync(int id, PassengerCreateUpdateDto updateDto)
    {
        var passenger = _mapper.Map<Passenger>(updateDto);
        passenger.Id = 0;
        var updated = await _repository.UpdateAsync(id, passenger);
        return updated is not null ? _mapper.Map<PassengerDto>(updated) : null;
    }

    /// <summary>
    /// Удалить пассажира.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
