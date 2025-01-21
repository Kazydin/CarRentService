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
            .ForMember(dest => dest.NumberOfCars, opt => opt.MapFrom(p => p.Cars.Count))
            .ForMember(dest => dest.NumberOfClients, opt => opt.MapFrom(p => p.Clients.Count))
            .ForMember(dest => dest.NumberOfManagers, opt => opt.MapFrom(p => p.Managers.Count));

        CreateMap<BranchDto, Branch>();
    }
}