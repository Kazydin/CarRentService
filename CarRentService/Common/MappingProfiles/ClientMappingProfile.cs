using AutoMapper;

using CarRentService.DAL.Entities;
using CarRentService.Home.Pages.Clients.Models;

namespace CarRentService.Common.MappingProfiles
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientDto>();
                //.ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => true)); // Значение по умолчанию

            CreateMap<ClientDto, Client>();
        }
    }
}
