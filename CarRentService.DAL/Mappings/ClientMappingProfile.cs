using AutoMapper;

using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, Client>();

        CreateMap<Client, ClientDto>();

        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id));
    }
}