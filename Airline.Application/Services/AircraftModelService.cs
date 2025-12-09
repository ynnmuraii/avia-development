using AutoMapper;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления моделями самолётов.
/// </summary>
public class AircraftModelService
{
    private readonly IRepository<AircraftModel> _modelRepository;
    private readonly IRepository<AircraftFamily> _familyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис моделей.
    /// </summary>
    public AircraftModelService(
        IRepository<AircraftModel> modelRepository,
        IRepository<AircraftFamily> familyRepository,
        IMapper mapper)
    {
        _modelRepository = modelRepository;
        _familyRepository = familyRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все модели.
    /// </summary>
    public async Task<IEnumerable<AircraftModelDto>> GetAllAsync()
    {
        var models = await _modelRepository.ReadAsync();
        return _mapper.Map<IEnumerable<AircraftModelDto>>(models);
    }

    /// <summary>
    /// Получить модель по идентификатору.
    /// </summary>
    public async Task<AircraftModelDto?> GetByIdAsync(int id)
    {
        var model = await _modelRepository.ReadByIdAsync(id);
        return model is not null ? _mapper.Map<AircraftModelDto>(model) : null;
    }

    /// <summary>
    /// Создать новую модель.
    /// </summary>
    public async Task<AircraftModelDto?> CreateAsync(AircraftModelCreateUpdateDto createDto)
    {
        var family = await _familyRepository.ReadByIdAsync(createDto.FamilyId);
        if (family is null)
            return null;

        var model = new AircraftModel
        {
            Id = 0,
            ModelName = createDto.ModelName,
            RangeKm = createDto.RangeKm,
            Seats = createDto.Seats,
            CargoCapacityKg = createDto.CargoCapacityKg,
            Family = family
        };

        var created = await _modelRepository.CreateAsync(model);
        return _mapper.Map<AircraftModelDto>(created);
    }

    /// <summary>
    /// Обновить данные модели.
    /// </summary>
    public async Task<AircraftModelDto?> UpdateAsync(int id, AircraftModelCreateUpdateDto updateDto)
    {
        var family = await _familyRepository.ReadByIdAsync(updateDto.FamilyId);
        if (family is null)
            return null;

        var model = new AircraftModel
        {
            Id = 0,
            ModelName = updateDto.ModelName,
            RangeKm = updateDto.RangeKm,
            Seats = updateDto.Seats,
            CargoCapacityKg = updateDto.CargoCapacityKg,
            Family = family
        };

        var updated = await _modelRepository.UpdateAsync(id, model);
        return updated is not null ? _mapper.Map<AircraftModelDto>(updated) : null;
    }

    /// <summary>
    /// Удалить модель.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        return await _modelRepository.DeleteAsync(id);
    }
}
