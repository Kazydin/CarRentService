using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class ManagerMappingProfile : Profile
{
    public ManagerMappingProfile()
    {
        CreateMap<Manager, Manager>();

        CreateMap<Manager, ManagerDto>();

        CreateMap<ManagerDto, Manager>();
    }
}