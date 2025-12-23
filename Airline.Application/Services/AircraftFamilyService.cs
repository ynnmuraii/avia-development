using AutoMapper;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления семействами самолётов.
/// </summary>
public class AircraftFamilyService(
    IRepository<AircraftFamily> repository,
    IMapper mapper) : IAircraftFamilyService
{
    /// <summary>
    /// Получить все семейства.
    /// </summary>
    public async Task<IEnumerable<AircraftFamilyDto>> GetAllAsync()
    {
        var families = await repository.ReadAsync();
        return mapper.Map<IEnumerable<AircraftFamilyDto>>(families);
    }

    /// <summary>
    /// Получить семейство по идентификатору.
    /// </summary>
    public async Task<AircraftFamilyDto?> GetByIdAsync(int id)
    {
        var family = await repository.ReadByIdAsync(id);
        return family is not null ? mapper.Map<AircraftFamilyDto>(family) : null;
    }

    /// <summary>
    /// Создать новое семейство.
    /// </summary>
    public async Task<AircraftFamilyDto> CreateAsync(AircraftFamilyCreateUpdateDto createDto)
    {
        var family = mapper.Map<AircraftFamily>(createDto);
        family.Id = 0;
        var created = await repository.CreateAsync(family);
        return mapper.Map<AircraftFamilyDto>(created);
    }

    /// <summary>
    /// Обновить данные семейства.
    /// </summary>
    public async Task UpdateAsync(int id, AircraftFamilyCreateUpdateDto updateDto)
    {
        var family = mapper.Map<AircraftFamily>(updateDto);
        family.Id = id;
        await repository.UpdateAsync(id, family);
    }

    /// <summary>
    /// Удалить семейство.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
