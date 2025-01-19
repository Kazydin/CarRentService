using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI(ServiceLifetime.Singleton)]
public interface IBranchService : ICrudService<Branch>
{
    BranchDto GetDto(int branchId);

    ObservableCollection<BranchDto> GetDtos();

    void IncludeCars(BranchDto dto);

    void IncludeClients(BranchDto dto);

    void IncludeManagers(BranchDto dto);
}