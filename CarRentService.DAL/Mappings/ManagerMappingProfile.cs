using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class ManagerMappingProfile : Profile
{
    public ManagerMappingProfile()
    {
        CreateMap<Manager, Manager>();

        CreateMap<Manager, ManagerDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.GetDescription()));

        CreateMap<ManagerDto, Manager>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToEnum<ManagerRoleEnum>()));
    }
}