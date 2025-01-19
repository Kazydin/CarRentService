using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Repositories;

[InjectDI]
public interface ICarRepository : ICrudRepository<Car>
{
    ObservableCollection<CarDto> GetDtos();

    CarDto GetDto(int entityId);

    void IncludeRentals(CarDto dto);

    void IncludeActiveRental(CarDto dto);

    void IncludeBranch(CarDto dto);

    void IncludeBranches(IEnumerable<CarDto> dtos);
}