using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Extensions;

public static class RentalExtensions
{
    public static double GetSeasonalRate(this RentalDto rental)
    {
        return rental.StartDate!.Value.Month switch
        {
            6 or 7 or 8 => 1.1, // Лето (июнь-август): увеличение на 10%
            9 or 10 or 11 => 1.0, // Осень (сентябрь-ноябрь): без надбавки
            12 or 1 or 2 => 1.2, // Зима (декабрь-февраль): увеличение на 20%
            3 or 4 or 5 => 0.9, // Весна (март-май): уменьшение на 10%
            _ => throw new ArgumentOutOfRangeException(nameof(rental.StartDate), "Неверный месяц для расчета ставки.")
        };
    }
}