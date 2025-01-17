using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IManagerService : ICrudService<Manager>
{
    ObservableCollection<ManagerDto> GetDtos();

    ManagerDto GetDto(int entityId);
}