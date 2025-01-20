using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        // Регистрационный номер: обязательное поле
        RuleFor(car => car.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Регистрационный номер обязателен.");
            // .Length(6, 10)
            // .WithMessage("Регистрационный номер должен содержать от 6 до 10 символов.");

        // Статус автомобиля
        RuleFor(car => car.Status)
            .IsInEnum()
            .WithMessage("Указан неверный статус автомобиля.");

        // Связь с филиалом
        RuleFor(car => car.Branch)
            .NotNull()
            .WithMessage("Филиал не может быть пустым.");

        // Мощность двигателя
        RuleFor(car => car.HorsePower)
            .GreaterThan(0)
            .WithMessage("Мощность автомобиля должна быть больше 0.")
            .LessThanOrEqualTo(1500)
            .WithMessage("Мощность автомобиля не может превышать 1500 лошадиных сил.");

        // Пробег
        RuleFor(car => car.Mileage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Пробег автомобиля не может быть отрицательным.");
            // .LessThanOrEqualTo(1_000_000)
            // .WithMessage("Пробег автомобиля не может превышать 1 000 000 км.");

        // Марка и модель: обязательные поля
        RuleFor(car => car.Make)
            .NotEmpty()
            .WithMessage("Марка автомобиля обязательна.");
        RuleFor(car => car.Model)
            .NotEmpty()
            .WithMessage("Модель автомобиля обязательна.");

        // Год выпуска
        RuleFor(car => car.Year)
            .InclusiveBetween(1886, DateTime.Now.Year)
            .WithMessage("Год выпуска должен быть в пределах от 1886 до текущего года.");

        // Список аренд
        RuleFor(car => car.Rentals)
            .NotNull()
            .WithMessage("Список аренд не может быть пустым.");
    }
}