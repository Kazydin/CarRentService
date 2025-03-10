﻿using AutoMapper;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, Payment>();

        CreateMap<Payment, PaymentDto>();

        CreateMap<PaymentDto, Payment>()
            .ForMember(dest => dest.Rental, opt => opt.Ignore());
    }
}