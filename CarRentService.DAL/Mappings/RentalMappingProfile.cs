using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class RentalMappingProfile : Profile
{
    public RentalMappingProfile()
    {
        CreateMap<Rental, Rental>();

        CreateMap<RentalDto, RentalDto>();

        CreateMap<Rental, RentalDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"№{src.Id} {src.Client.Fio} от {src.StartDate:dd.MM.yyyy}"));

        CreateMap<RentalDto, Rental>()
            .ForMember(dest => dest.Cars, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Insurances, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore());
    }
}