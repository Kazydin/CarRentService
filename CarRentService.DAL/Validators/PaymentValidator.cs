using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {

    }
}