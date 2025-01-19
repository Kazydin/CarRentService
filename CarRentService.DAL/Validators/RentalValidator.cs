using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class RentalValidator : AbstractValidator<Rental>
{
    public RentalValidator()
    {

    }
}