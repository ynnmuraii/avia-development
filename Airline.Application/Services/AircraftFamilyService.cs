using AutoMapper;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления семействами самолётов.
/// </summary>
public class AircraftFamilyService : IAircraftFamilyService
{
    private readonly IRepository<AircraftFamily> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис семейств.
    /// </summary>
    public AircraftFamilyService(IRepository<AircraftFamily> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все семейства.
    /// </summary>
    public async Task<IEnumerable<AircraftFamilyDto>> GetAllAsync()
    {
        var families = await _repository.ReadAsync();
        return _mapper.Map<IEnumerable<AircraftFamilyDto>>(families);
    }

    /// <summary>
    /// Получить семейство по идентификатору.
    /// </summary>
    public async Task<AircraftFamilyDto?> GetByIdAsync(int id)
    {
        var family = await _repository.ReadByIdAsync(id);
        return family is not null ? _mapper.Map<AircraftFamilyDto>(family) : null;
    }

    /// <summary>
    /// Создать новое семейство.
    /// </summary>
    public async Task<AircraftFamilyDto> CreateAsync(AircraftFamilyCreateUpdateDto createDto)
    {
        var family = _mapper.Map<AircraftFamily>(createDto);
        family.Id = 0;
        var created = await _repository.CreateAsync(family);
        return _mapper.Map<AircraftFamilyDto>(created);
    }

    /// <summary>
    /// Обновить данные семейства.
    /// </summary>
    public async Task<AircraftFamilyDto?> UpdateAsync(int id, AircraftFamilyCreateUpdateDto updateDto)
    {
        var family = _mapper.Map<AircraftFamily>(updateDto);
        family.Id = 0;
        var updated = await _repository.UpdateAsync(id, family);
        return updated is not null ? _mapper.Map<AircraftFamilyDto>(updated) : null;
    }

    /// <summary>
    /// Удалить семейство.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
