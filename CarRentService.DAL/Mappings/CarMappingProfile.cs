using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        CreateMap<Car, Car>();

        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.CurrentRental, opt => opt.MapFrom(src => src.Rentals.FirstOrDefault(p => p.Status == RentalStatusEnum.Active)));

        CreateMap<CarDto, Car>()
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Rentals, opt => opt.Ignore());
    }
}