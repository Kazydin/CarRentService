using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Repositories;

[InjectDI]
public interface IInsuranceRepository : ICrudRepository<Insurance>
{
    ObservableCollection<InsuranceDto> GetDtos();

    InsuranceDto GetDto(int entityId);

    void IncludeRental(InsuranceDto dto);

    void IncludeCar(InsuranceDto dto);

    void IncludeCars(IEnumerable<InsuranceDto> dtos);
}