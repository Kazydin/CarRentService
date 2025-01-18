using AutoMapper;

using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, Client>();

        // TODO: попробовать удалить
        CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.CurrentCars, opt => opt.Ignore());

        CreateMap<ClientDto, Client>();
    }
}