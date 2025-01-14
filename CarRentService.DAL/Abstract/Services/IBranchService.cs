using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IBranchService : ICrudService<Branch>
{
    public BranchDto GetBranchDto(int branchId);

    public ObservableCollection<BranchDto> GetAllDtos();
}