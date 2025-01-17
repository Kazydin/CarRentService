using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class InsuranceMappingProfile : Profile
{
    public InsuranceMappingProfile()
    {
        CreateMap<Insurance, Insurance>();

        CreateMap<Insurance, InsuranceDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.GetDescription()));

        CreateMap<InsuranceDto, Insurance>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnumFromDescription<InsuranceTypeEnum>()));
    }
}