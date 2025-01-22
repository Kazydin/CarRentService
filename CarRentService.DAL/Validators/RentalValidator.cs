using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class RentalValidator : AbstractValidator<Rental>
{
    public RentalValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(r => r.Client)
            .NotNull().WithMessage("Клиент обязателен.");

        RuleFor(r => r.Branch)
            .NotNull().WithMessage("Филиал обязателен.");

        RuleFor(r => r.Cars)
            .NotEmpty().WithMessage("Должен быть выбран хотя бы один автомобиль.");

        RuleFor(r => r.StartDate)
            .NotEmpty().WithMessage("Дата начала аренды обязательная")
            .GreaterThan(DateTime.Now).WithMessage("Дата начала аренды должна быть в будущем")
            .LessThan(r => r.EndDate)
            .WithMessage("Дата начала аренды должна быть раньше даты окончания.");

        RuleFor(r => r.EndDate)
            .NotEmpty().WithMessage("Дата окончания обязательная")
            .GreaterThan(DateTime.Now)
            .WithMessage("Дата окончания аренды должна быть в будущем.");

        RuleFor(r => r.Status)
            .IsInEnum().WithMessage("Неверный статус аренды.");

        RuleFor(r => r.Tariff)
            .IsInEnum().WithMessage("Неверный тариф аренды.");

        RuleFor(r => r.TotalCost)
            .GreaterThanOrEqualTo(0).WithMessage("Стоимость аренды не может быть отрицательной.");

        RuleForEach(r => r.Insurances)
            .SetValidator(new InsuranceValidator());
    }
}