using AutoMapper;
using CarRentService.DAL.Entities;

namespace CarRentService.Common.MappingProfiles;

class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, Client>();
    }
}