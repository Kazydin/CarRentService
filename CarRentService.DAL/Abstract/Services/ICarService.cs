using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface ICarService : ICrudService<Car>
{
    ObservableCollection<CarDto> GetDtos();

    CarDto GetDto(int entityId);
}