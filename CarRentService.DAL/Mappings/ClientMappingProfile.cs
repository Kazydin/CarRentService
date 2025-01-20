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
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Fio} ({src.DriverLicenseNumber})"));

        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Rentals, opt => opt.Ignore());
    }
}