using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Seeding;

public class InsuranceSeeder(IInsuranceService service) : ISeeder
{
    public SeederType SeederType => SeederType.Insurance;

    public void Seed()
    {
        service.Add(new Insurance
        {
            CarId = 1,
            RentalId = 1,
            Type = InsuranceTypeEnum.Full,
            Cost = 2000
        });

        service.Add(new Insurance
        {
            CarId = 2,
            RentalId = 2,
            Type = InsuranceTypeEnum.Partial,
            Cost = 3000
        });
    }
}