using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class ManagerValidator : AbstractValidator<Manager>
{
    public ManagerValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(manager => manager.Fio)
            .NotEmpty().WithMessage("ФИО не должно быть пустым.")
            .MinimumLength(5).WithMessage("ФИО должно содержать не менее 5 символов.")
            .MaximumLength(100).WithMessage("ФИО должно содержать не более 100 символов.");

        RuleFor(manager => manager.Age)
            .InclusiveBetween(18, 100).WithMessage("Возраст должен быть в диапазоне от 18 до 100 лет.");

        RuleFor(manager => manager.Phone)
            .NotEmpty().WithMessage("Телефон не должен быть пустым.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.");

        RuleFor(manager => manager.Login)
            .NotEmpty().WithMessage("Логин не должен быть пустым.")
            .MinimumLength(3).WithMessage("Логин должен содержать не менее 3 символов.")
            .MaximumLength(50).WithMessage("Логин должен содержать не более 50 символов.");

        RuleFor(manager => manager.Password)
            .NotEmpty().WithMessage("Пароль не должен быть пустым.")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов.")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
            .Matches(@"[\W]").WithMessage("Пароль должен содержать хотя бы один специальный символ.");

        RuleFor(manager => manager.Branches)
            .Must(branches => branches != null && branches.Count >= 1)
            .WithMessage("Менеджер филиала должен иметь хотя бы одну привязку к филиалу")
            .When(manager => manager.Role == ManagerRoleEnum.BranchManager);
    }
}