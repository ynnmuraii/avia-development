using Airline.API.Services;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Application.Contracts.Services;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Airline.API.Consumers;

/// <summary>
/// Консьюмер сообщений для создания семейств самолётов.
/// </summary>
public class AircraftFamilyConsumer : RabbitMqConsumerBase<CreateAircraftFamilyMessage>
{
    public AircraftFamilyConsumer(
        ILogger<AircraftFamilyConsumer> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
        : base(logger, serviceProvider, configuration, QueueNames.AircraftFamilies)
    {
    }

    protected override async Task ProcessMessageAsync(CreateAircraftFamilyMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var familyService = scope.ServiceProvider.GetRequiredService<IAircraftFamilyService>();

        var dto = new AircraftFamilyCreateUpdateDto(message.Manufacturer, message.FamilyName);

        await familyService.CreateAsync(dto);
        Logger.LogInformation("Семейство самолётов создано: {Manufacturer} {FamilyName}", message.Manufacturer, message.FamilyName);
    }
}
