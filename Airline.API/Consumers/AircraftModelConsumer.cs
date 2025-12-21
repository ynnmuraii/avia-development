using Airline.API.Services;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.Services;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Airline.API.Consumers;

/// <summary>
/// Консьюмер сообщений для создания моделей самолётов.
/// </summary>
public class AircraftModelConsumer : RabbitMqConsumerBase<CreateAircraftModelMessage>
{
    public AircraftModelConsumer(
        ILogger<AircraftModelConsumer> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
        : base(logger, serviceProvider, configuration, QueueNames.AircraftModels)
    {
    }

    protected override async Task ProcessMessageAsync(CreateAircraftModelMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var modelService = scope.ServiceProvider.GetRequiredService<IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto>>();

        var dto = new AircraftModelCreateUpdateDto(
            message.ModelName,
            message.RangeKm,
            message.Seats,
            message.CargoCapacityKg,
            message.FamilyId);

        await modelService.CreateAsync(dto);
        Logger.LogInformation("Модель самолёта создана: {ModelName}", message.ModelName);
    }
}
