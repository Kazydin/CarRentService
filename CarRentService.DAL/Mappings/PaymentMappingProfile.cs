using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, Payment>();

        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.Rental, opt => opt.MapFrom(src => src.Rental));

        CreateMap<PaymentDto, Payment>();
    }
}