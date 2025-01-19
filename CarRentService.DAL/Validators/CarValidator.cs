using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {

    }
}