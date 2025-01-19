using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Repositories;

[InjectDI]
public interface IManagerRepository : ICrudRepository<Manager>
{
    ObservableCollection<ManagerDto> GetDtos();

    ManagerDto GetDto(int entityId);

    void IncludeBranches(ManagerDto dto);
}