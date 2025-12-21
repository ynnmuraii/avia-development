using AutoMapper;
using Airline.Application.Contracts.Tickets;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления билетами.
/// </summary>
public class TicketService : IApplicationService<TicketDto, TicketCreateUpdateDto>
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<Flight> _flightRepository;
    private readonly IRepository<Passenger> _passengerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис билетов.
    /// </summary>
    public TicketService(
        IRepository<Ticket> ticketRepository,
        IRepository<Flight> flightRepository,
        IRepository<Passenger> passengerRepository,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все билеты.
    /// </summary>
    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.ReadAsync();
        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    /// <summary>
    /// Получить билет по идентификатору.
    /// </summary>
    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketRepository.ReadByIdAsync(id);
        return ticket is not null ? _mapper.Map<TicketDto>(ticket) : null;
    }

    /// <summary>
    /// Создать новый билет.
    /// </summary>
    public async Task<TicketDto> CreateAsync(TicketCreateUpdateDto createDto)
    {
        var flight = await _flightRepository.ReadByIdAsync(createDto.FlightId);
        var passenger = await _passengerRepository.ReadByIdAsync(createDto.PassengerId);

        if (flight is null)
            throw new InvalidOperationException($"Flight with id {createDto.FlightId} not found");
        if (passenger is null)
            throw new InvalidOperationException($"Passenger with id {createDto.PassengerId} not found");

        var ticket = new Ticket
        {
            Id = 0,
            Flight = flight,
            Passenger = passenger,
            SeatId = createDto.SeatId,
            HasCarryOn = createDto.HasCarryOn,
            BaggageKg = createDto.BaggageKg
        };

        var created = await _ticketRepository.CreateAsync(ticket);
        return _mapper.Map<TicketDto>(created);
    }

    /// <summary>
    /// Обновить данные билета.
    /// </summary>
    public async Task UpdateAsync(int id, TicketCreateUpdateDto updateDto)
    {
        var flight = await _flightRepository.ReadByIdAsync(updateDto.FlightId);
        var passenger = await _passengerRepository.ReadByIdAsync(updateDto.PassengerId);

        if (flight is null)
            throw new InvalidOperationException($"Flight with id {updateDto.FlightId} not found");
        if (passenger is null)
            throw new InvalidOperationException($"Passenger with id {updateDto.PassengerId} not found");

        var ticket = new Ticket
        {
            Id = id,
            Flight = flight,
            Passenger = passenger,
            SeatId = updateDto.SeatId,
            HasCarryOn = updateDto.HasCarryOn,
            BaggageKg = updateDto.BaggageKg
        };

        await _ticketRepository.UpdateAsync(id, ticket);
    }

    /// <summary>
    /// Удалить билет.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await _ticketRepository.DeleteAsync(id);
    }
}
