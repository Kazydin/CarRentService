using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class InsuranceMappingProfile : Profile
{
    public InsuranceMappingProfile()
    {
        CreateMap<Insurance, Insurance>();

        CreateMap<Insurance, InsuranceDto>();

        CreateMap<InsuranceDto, Insurance>()
            .ForMember(dest => dest.Car, opt => opt.Ignore())
            .ForMember(dest => dest.Rental, opt => opt.Ignore());

    }
}