using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class ManagerValidator : AbstractValidator<Manager>
{
    public ManagerValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(client => client.Fio)
            .NotEmpty().WithMessage("ФИО не должно быть пустым.")
            .MinimumLength(5).WithMessage("ФИО должно содержать не менее 5 символов.")
            .MaximumLength(100).WithMessage("ФИО должно содержать не более 100 символов.");

        RuleFor(client => client.Age)
            .InclusiveBetween(18, 100).WithMessage("Возраст должен быть в диапазоне от 18 до 100 лет.");

        RuleFor(client => client.Phone)
            .NotEmpty().WithMessage("Телефон не должен быть пустым.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.");

        RuleFor(client => client.Login)
            .NotEmpty().WithMessage("Логин не должен быть пустым.")
            .MinimumLength(3).WithMessage("Логин должен содержать не менее 3 символов.")
            .MaximumLength(50).WithMessage("Логин должен содержать не более 50 символов.");

        RuleFor(client => client.Password)
            .NotEmpty().WithMessage("Пароль не должен быть пустым.")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов.")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
            .Matches(@"[\W]").WithMessage("Пароль должен содержать хотя бы один специальный символ.");
    }
}