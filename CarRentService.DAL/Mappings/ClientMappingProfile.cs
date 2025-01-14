using AutoMapper;

using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, Client>();

        CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.CurrentCars, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Rentals.SelectMany(r => r.Payments)))
            .ForMember(dest => dest.Insurances, opt => opt.MapFrom(src => src.Rentals.SelectMany(r => r.Insurances)));

        CreateMap<ClientDto, Client>();
    }
}