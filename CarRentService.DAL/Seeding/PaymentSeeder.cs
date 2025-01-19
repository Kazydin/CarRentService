using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Seeding;

public class PaymentSeeder(IPaymentRepository repository) : ISeeder
{
    public SeederType SeederType => SeederType.Payment;

    public void Seed()
    {
        repository.Add(new Payment
        {
            RentalId = 1,
            Amount = 90,
            Date = DateTime.Parse("23.12.2024 13:35"),
            Method = PaymentMethodEnum.Card
        });

        repository.Add(new Payment
        {
            RentalId = 2,
            Amount = 3000,
            Date = DateTime.Parse("11.11.2024 10:25"),
            Method = PaymentMethodEnum.Cash
        });
    }
}