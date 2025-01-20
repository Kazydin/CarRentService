using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Seeding;

public class InsuranceSeeder : ISeeder
{
    public SeederType SeederType => SeederType.Insurance;

    public void Seed()
    {
        // repository.Add(new Insurance
        // {
        //     CarId = 1,
        //     RentalId = 1,
        //     Type = InsuranceTypeEnum.FullCoverage,
        //     Cost = 2000
        // });
        //
        // repository.Add(new Insurance
        // {
        //     CarId = 2,
        //     RentalId = 2,
        //     Type = InsuranceTypeEnum.SeasonalCoverage,
        //     Cost = 3000
        // });
    }
}