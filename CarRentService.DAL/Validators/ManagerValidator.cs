using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class ManagerValidator : AbstractValidator<Manager>
{
    public ManagerValidator()
    {

    }
}