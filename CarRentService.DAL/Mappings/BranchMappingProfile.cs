using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class BranchMappingProfile : Profile
{
    public BranchMappingProfile()
    {
        CreateMap<Branch, Branch>();

        CreateMap<Branch, BranchDto>();

        CreateMap<BranchDto, Branch>();
    }
}