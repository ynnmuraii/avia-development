using Airline.Application.Contracts.Services;
using Airline.Application.Contracts.Tickets;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Airline.Infrastructure.EfCore.Messaging;

/// <summary>
/// Консьюмер сообщений для создания билетов.
/// </summary>
public class TicketConsumer : RabbitMqConsumerBase<CreateTicketMessage>
{
    public TicketConsumer(
        ILogger<TicketConsumer> logger,
        IServiceProvider serviceProvider,
        IConnection connection)
        : base(logger, serviceProvider, connection, QueueNames.Tickets)
    {
    }

    /// <summary>
    /// Обрабатывает сообщение о создании билета.
    /// </summary>
    /// <param name="message">Сообщение с данными билета.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    protected override async Task ProcessMessageAsync(CreateTicketMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var ticketService = scope.ServiceProvider.GetRequiredService<IApplicationService<TicketDto, TicketCreateUpdateDto>>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var dto = mapper.Map<TicketCreateUpdateDto>(message);

        try
        {
            await ticketService.CreateAsync(dto);
            Logger.LogInformation("Билет создан: рейс {FlightId}, пассажир {PassengerId}", message.FlightId, message.PassengerId);
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogWarning("Не удалось создать билет: {Message}. Рейс {FlightId}, пассажир {PassengerId}",
                ex.Message, message.FlightId, message.PassengerId);
        }
    }
}
