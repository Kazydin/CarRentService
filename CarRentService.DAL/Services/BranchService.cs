using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using FluentValidation;
using GuardNet;

namespace CarRentService.DAL.Services;

public class BranchService : BaseCrudService<Branch>, IBranchService
{
    public sealed override ObservableCollection<Branch> Table { get; set; }

    public BranchService(IDataStoreContext store,
        IValidator<Branch> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
        Table = _store.Branch;
    }

    public override Branch? TryFindById(int id)
    {
        return _store.Branch.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Branch entity)
    {
    }

    public BranchDto GetDto(int branchId)
    {
        var entity = _store.Branch.FirstOrDefault(p => p.Id == branchId);

        Guard.NotNull(entity, nameof(entity), $"Филиал с ID {branchId} не найден");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Branch>(entity);

        var dto = _mapper.Map<BranchDto>(entity);

        IncludeCars(dto);
        IncludeClients(dto);
        IncludeClients(dto);
        IncludeManagers(dto);

        dto.NumberOfCars = dto.Cars.Count;

        return dto;
    }

    public ObservableCollection<BranchDto> GetDtos()
    {
        return Table.Select(p => GetDto(p.Id)).ToObservableCollection();
    }

    public void IncludeCars(BranchDto dto)
    {
        dto.Cars = _mapper.Map<ObservableCollection<CarDto>>(_store.Car.Where(p => p.BranchId == dto.Id));
    }

    public void IncludeClients(BranchDto dto)
    {
        dto.Clients = _mapper.Map<ObservableCollection<ClientDto>>(_store.Client.Where(p => p.BranchId == dto.Id));
    }

    public void IncludeManagers(BranchDto dto)
    {
        dto.Managers = _mapper.Map<ObservableCollection<ManagerDto>>(_store.Manager.Where(p =>
            p.Role == ManagerRoleEnum.BranchManager && p.BranchIds.Contains(dto.Id!.Value)));
    }
}