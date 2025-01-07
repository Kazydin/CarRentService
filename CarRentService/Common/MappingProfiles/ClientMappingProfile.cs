using AutoMapper;

using CarRentService.DAL.Entities;
using ClientDto = CarRentService.Pages.Clients.Models.ClientDto;

namespace CarRentService.Common.MappingProfiles
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientDto>();

            CreateMap<ClientDto, Client>();
        }
    }
}
