using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Seeding;

public class RentalSeeder : ISeeder
{
    public SeederType SeederType => SeederType.Rental;

    public void Seed()
    {
        
    }
}