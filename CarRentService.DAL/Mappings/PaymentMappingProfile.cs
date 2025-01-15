using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, Payment>();

        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.GetDescription()));

        CreateMap<PaymentDto, Payment>()
            .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.ToEnumFromDescription<PaymentMethodEnum>()));
    }
}