using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class BranchValidator : AbstractValidator<Client>
{
    public BranchValidator()
    {
        
    }
}