using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class InsuranceValidator : AbstractValidator<Insurance>
{
    public InsuranceValidator()
    {

    }
}