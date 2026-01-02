using AutoMapper;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления моделями самолётов.
/// </summary>
public class AircraftModelService(
    IRepository<AircraftModel> modelRepository,
    IRepository<AircraftFamily> familyRepository,
    IMapper mapper) : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto>
{
    /// <summary>
    /// Получить все модели.
    /// </summary>
    public async Task<IEnumerable<AircraftModelDto>> GetAllAsync()
    {
        var models = await modelRepository.ReadAsync();
        return mapper.Map<IEnumerable<AircraftModelDto>>(models);
    }

    /// <summary>
    /// Получить модель по идентификатору.
    /// </summary>
    public async Task<AircraftModelDto?> GetByIdAsync(int id)
    {
        var model = await modelRepository.ReadByIdAsync(id);
        return model is not null ? mapper.Map<AircraftModelDto>(model) : null;
    }

    /// <summary>
    /// Создать новую модель.
    /// </summary>
    public async Task<AircraftModelDto> CreateAsync(AircraftModelCreateUpdateDto createDto)
    {
        var family = await familyRepository.ReadByIdAsync(createDto.FamilyId);
        if (family is null)
            throw new InvalidOperationException($"Aircraft family with id {createDto.FamilyId} not found");

        var model = new AircraftModel
        {
            Id = 0,
            ModelName = createDto.ModelName,
            RangeKm = createDto.RangeKm,
            Seats = createDto.Seats,
            CargoCapacityKg = createDto.CargoCapacityKg,
            Family = family
        };

        var created = await modelRepository.CreateAsync(model);
        return mapper.Map<AircraftModelDto>(created);
    }

    /// <summary>
    /// Обновить данные модели.
    /// </summary>
    public async Task UpdateAsync(int id, AircraftModelCreateUpdateDto updateDto)
    {
        var family = await familyRepository.ReadByIdAsync(updateDto.FamilyId);
        if (family is null)
            throw new InvalidOperationException($"Aircraft family with id {updateDto.FamilyId} not found");

        var model = new AircraftModel
        {
            Id = id,
            ModelName = updateDto.ModelName,
            RangeKm = updateDto.RangeKm,
            Seats = updateDto.Seats,
            CargoCapacityKg = updateDto.CargoCapacityKg,
            Family = family
        };

        await modelRepository.UpdateAsync(id, model);
    }

    /// <summary>
    /// Удалить модель.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await modelRepository.DeleteAsync(id);
    }
}
