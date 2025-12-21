using Airline.API.Services;
using Airline.Application.Contracts.Services;
using Airline.Application.Contracts.Tickets;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Airline.API.Consumers;

/// <summary>
/// Консьюмер сообщений для создания билетов.
/// </summary>
public class TicketConsumer : RabbitMqConsumerBase<CreateTicketMessage>
{
    public TicketConsumer(
        ILogger<TicketConsumer> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
        : base(logger, serviceProvider, configuration, QueueNames.Tickets)
    {
    }

    protected override async Task ProcessMessageAsync(CreateTicketMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var ticketService = scope.ServiceProvider.GetRequiredService<IApplicationService<TicketDto, TicketCreateUpdateDto>>();

        var dto = new TicketCreateUpdateDto(
            message.FlightId,
            message.PassengerId,
            message.SeatId,
            message.HasCarryOn,
            message.BaggageKg);

        await ticketService.CreateAsync(dto);
        Logger.LogInformation("Билет создан: рейс {FlightId}, пассажир {PassengerId}", message.FlightId, message.PassengerId);
    }
}
