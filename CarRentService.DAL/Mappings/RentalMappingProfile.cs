using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class RentalMappingProfile : Profile
{
    public RentalMappingProfile()
    {
        CreateMap<Rental, Rental>();

        CreateMap<Rental, RentalDto>();

        CreateMap<RentalDto, Rental>();
    }
}