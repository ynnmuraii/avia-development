using Airline.API.Services;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Services;
using Airline.Messaging.Contracts;
using Airline.Messaging.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Airline.API.Consumers;

/// <summary>
/// Консьюмер сообщений для создания пассажиров.
/// </summary>
public class PassengerConsumer : RabbitMqConsumerBase<CreatePassengerMessage>
{
    public PassengerConsumer(
        ILogger<PassengerConsumer> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
        : base(logger, serviceProvider, configuration, QueueNames.Passengers)
    {
    }

    protected override async Task ProcessMessageAsync(CreatePassengerMessage message, CancellationToken cancellationToken)
    {
        using var scope = ServiceProvider.CreateScope();
        var passengerService = scope.ServiceProvider.GetRequiredService<IApplicationService<PassengerDto, PassengerCreateUpdateDto>>();

        var dto = new PassengerCreateUpdateDto
        {
            FirstName = message.FirstName,
            LastName = message.LastName,
            Patronymic = message.Patronymic,
            PassportNumber = message.PassportNumber,
            BirthDate = message.BirthDate
        };

        await passengerService.CreateAsync(dto);
        Logger.LogInformation("Пассажир создан: {FirstName} {LastName}", message.FirstName, message.LastName);
    }
}
