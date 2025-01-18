using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        CreateMap<Car, Car>();

        CreateMap<Car, CarDto>();

        CreateMap<CarDto, Car>();
    }
}