using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;

namespace CarRentService.DAL.Seeding;

public class InsuranceSeeder(IInsuranceService service) : ISeeder
{
    public SeederType SeederType => SeederType.Insurance;

    public void Seed()
    {

    }
}