using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface ICarService : ICrudService<Car>
{
    ObservableCollection<CarDto> GetDtos();

    CarDto GetDto(int entityId);

    void IncludeRentals(CarDto dto);

    void IncludeActiveRental(CarDto dto);

    void IncludeBranch(CarDto dto);
}