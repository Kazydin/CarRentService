using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class BranchMappingProfile : Profile
{
    public BranchMappingProfile()
    {
        CreateMap<Branch, Branch>();

        CreateMap<Branch, BranchDto>()
            .ForMember(dest => dest.NumberOfCars, opt => opt.MapFrom(p => p.Cars.Count));

        CreateMap<BranchDto, Branch>();
    }
}