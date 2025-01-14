using AutoMapper;

using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));

        CreateMap<CarDto, Car>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<CarStatusEnum>()));
    }
}