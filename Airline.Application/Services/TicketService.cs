using AutoMapper;
using Airline.Application.Contracts.Tickets;
using Airline.Application.Contracts.Services;
using Airline.Domain;
using Airline.Domain.Repositories;

namespace Airline.Application.Services;

/// <summary>
/// Сервис для управления билетами.
/// </summary>
public class TicketService(
    ITicketRepository ticketRepository,
    IRepository<Flight> flightRepository,
    IRepository<Passenger> passengerRepository,
    IMapper mapper) : ITicketService
{
    /// <summary>
    /// Получить все билеты.
    /// </summary>
    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        var tickets = await ticketRepository.ReadAsync();
        return mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    /// <summary>
    /// Получить билет по идентификатору.
    /// </summary>
    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await ticketRepository.ReadByIdAsync(id);
        return ticket is not null ? mapper.Map<TicketDto>(ticket) : null;
    }

    /// <summary>
    /// Получить все билеты для указанного рейса.
    /// </summary>
    /// <param name="flightId">Идентификатор рейса.</param>
    /// <returns>Список DTO билетов.</returns>
    public async Task<List<TicketDto>> GetTicketsByFlightIdAsync(int flightId)
    {
        var tickets = await ticketRepository.GetByFlightIdAsync(flightId);
        return mapper.Map<List<TicketDto>>(tickets.ToList());
    }

    /// <summary>
    /// Создать новый билет.
    /// </summary>
    public async Task<TicketDto> CreateAsync(TicketCreateUpdateDto createDto)
    {
        var flight = await flightRepository.ReadByIdAsync(createDto.FlightId);
        var passenger = await passengerRepository.ReadByIdAsync(createDto.PassengerId);

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

        var created = await ticketRepository.CreateAsync(ticket);
        return mapper.Map<TicketDto>(created);
    }

    /// <summary>
    /// Обновить данные билета.
    /// </summary>
    public async Task UpdateAsync(int id, TicketCreateUpdateDto updateDto)
    {
        var flight = await flightRepository.ReadByIdAsync(updateDto.FlightId);
        var passenger = await passengerRepository.ReadByIdAsync(updateDto.PassengerId);

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

        await ticketRepository.UpdateAsync(id, ticket);
    }

    /// <summary>
    /// Удалить билет.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await ticketRepository.DeleteAsync(id);
    }
}
