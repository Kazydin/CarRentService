using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class RentalMappingProfile : Profile
{
    public RentalMappingProfile()
    {
        CreateMap<Rental, Rental>();

        CreateMap<Rental, RentalDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()))
            .ForMember(dest => dest.Tariff, opt => opt.MapFrom(src => src.Tariff.GetDescription()));

        CreateMap<RentalDto, Rental>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnumFromDescription<RentalStatusEnum>()))
            .ForMember(dest => dest.Tariff, opt => opt.MapFrom(src => src.Tariff.ToEnumFromDescription<RentalTariffEnum>()));
    }
}