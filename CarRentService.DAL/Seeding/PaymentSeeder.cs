using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;

namespace CarRentService.DAL.Seeding;

public class PaymentSeeder(IPaymentService service) : ISeeder
{
    public SeederType SeederType => SeederType.Payment;

    public void Seed()
    {

    }
}