using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class RentalMappingProfile : Profile
{
    public RentalMappingProfile()
    {
        CreateMap<Rental, RentalDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));

        CreateMap<RentalDto, Rental>();
    }
}