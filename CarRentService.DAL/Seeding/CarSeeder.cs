using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;

namespace CarRentService.DAL.Seeding;

public class CarSeeder(ICarService service) : ISeeder
{
    public SeederType SeederType => SeederType.Car;

    public void Seed()
    {

    }
}