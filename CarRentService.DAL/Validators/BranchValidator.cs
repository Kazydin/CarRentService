using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        // Название филиала не может быть пустым или короче 3 символов
        RuleFor(branch => branch.Name)
            .NotEmpty().WithMessage("Название филиала обязательно.")
            .MinimumLength(3).WithMessage("Название филиала должно содержать минимум 3 символа.")
            .MaximumLength(100).WithMessage("Название филиала не может превышать 100 символов.");

        // Адрес филиала обязателен
        RuleFor(branch => branch.Address)
            .NotEmpty().WithMessage("Адрес филиала обязателен.")
            .MinimumLength(10).WithMessage("Адрес филиала должен содержать минимум 10 символов.")
            .MaximumLength(200).WithMessage("Адрес филиала не может превышать 200 символов.");

        // Контактные данные филиала
        RuleFor(branch => branch.ContactDetails)
            .NotEmpty().WithMessage("Контактные данные филиала обязательны.")
            .MinimumLength(10).WithMessage("Контактные данные филиала должны содержать минимум 10 символов.")
            .MaximumLength(200).WithMessage("Контактные данные филиала не могут превышать 200 символов.");
    }
}