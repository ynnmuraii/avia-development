using Airline.API.Services;
using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Services;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Airline.API.Consumers;

/// <summary>
/// Консьюмер сообщений для создания рейсов.
/// </summary>
public class FlightConsumer : RabbitMqConsumerBase<CreateFlightMessage>
{
    public FlightConsumer(
        ILogger<FlightConsumer> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
        : base(logger, serviceProvider, configuration, QueueNames.Flights)
    {
    }

    protected override async Task ProcessMessageAsync(CreateFlightMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var flightService = scope.ServiceProvider.GetRequiredService<IApplicationService<FlightDto, FlightCreateUpdateDto>>();

        var dto = new FlightCreateUpdateDto(
            message.Code,
            message.From,
            message.To,
            message.DateOfDeparture,
            message.DateOfArrival,
            message.TimeOfDeparture,
            message.FlightDuration,
            message.ModelId);

        await flightService.CreateAsync(dto);
        Logger.LogInformation("Рейс создан: {Code}", message.Code);
    }
}
