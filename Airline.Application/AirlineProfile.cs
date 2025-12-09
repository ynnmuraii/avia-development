using Airline.Application.Contracts.Flights;
using Airline.Application.Contracts.Passengers;
using Airline.Application.Contracts.Tickets;
using Airline.Application.Contracts.AircraftModels;
using Airline.Application.Contracts.AircraftFamilies;
using Airline.Domain;
using AutoMapper;

namespace Airline.Application;

/// <summary>
/// Профиль маппинга AutoMapper для преобразования между сущностями и DTOs.
/// </summary>
public class AirlineProfile : Profile
{
    /// <summary>
    /// Инициализирует профиль маппинга.
    /// </summary>
    public AirlineProfile()
    {
        // Flight mapping
        CreateMap<Flight, FlightDto>()
            .ForMember(
                dest => dest.ModelId,
                opt => opt.MapFrom(src => src.Model.Id)
            );
        
        CreateMap<FlightCreateUpdateDto, Flight>()
            .ForMember(
                dest => dest.Model,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            );

        // Passenger mapping
        CreateMap<Passenger, PassengerDto>();
        CreateMap<PassengerCreateUpdateDto, Passenger>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            );

        // Ticket mapping
        CreateMap<Ticket, TicketDto>()
            .ForMember(
                dest => dest.FlightId,
                opt => opt.MapFrom(src => src.Flight.Id)
            )
            .ForMember(
                dest => dest.PassengerId,
                opt => opt.MapFrom(src => src.Passenger.Id)
            );
        
        CreateMap<TicketCreateUpdateDto, Ticket>()
            .ForMember(
                dest => dest.Flight,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.Passenger,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            );

        // AircraftModel mapping
        CreateMap<AircraftModel, AircraftModelDto>()
            .ForMember(
                dest => dest.FamilyId,
                opt => opt.MapFrom(src => src.Family.Id)
            );
        
        CreateMap<AircraftModelCreateUpdateDto, AircraftModel>()
            .ForMember(
                dest => dest.Family,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            );

        // AircraftFamily mapping
        CreateMap<AircraftFamily, AircraftFamilyDto>();
        CreateMap<AircraftFamilyCreateUpdateDto, AircraftFamily>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            );
    }
}
