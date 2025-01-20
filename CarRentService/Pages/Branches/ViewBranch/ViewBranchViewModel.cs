using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.Data;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Branches.ViewBranch;

public partial class ViewBranchViewModel : BaseViewModel
{
    public RelayCommand DeleteBranchCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand AddCarCommand { get; }

    public RelayCommand<object> EditCarCommand { get; }

    public RelayCommand AddClientCommand { get; }

    public RelayCommand<object> EditClientCommand { get; }

    public RelayCommand AddManagerCommand { get; }

    public RelayCommand<object> EditManagerCommand { get; }

    public RelayCommand<object> DeleteManagerCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private BranchDto _branch;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly AppDbContext _store;

    private readonly AppState _appState;

    public ViewBranchViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IUniversalMapper<BranchDto, Branch> branchMapper,
        AppDbContext store,
        AppState appState)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _branchMapper = branchMapper;
        _store = store;
        _appState = appState;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteBranchCommand = new RelayCommand(DeleteBranch, CanDeleteBranch);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddCarCommand = new RelayCommand(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);

        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);

        AddManagerCommand = new RelayCommand(AddManager, CanAddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
        DeleteManagerCommand = new RelayCommand<object>(DeleteManager);
    }

    private void AddCar()
    {
        _navigationService.Navigate(PageTypeEnum.EditCar);
    }

    private void EditCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditCar, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void AddClient()
    {
        _navigationService.Navigate(PageTypeEnum.EditClient);
    }

    private void EditClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Client record)
        {
            _navigationService.Navigate(PageTypeEnum.EditClient, parameters: new CommonNavigationData(record.Id));
        }
    }

    private void AddManager()
    {
        _navigationService.Navigate(PageTypeEnum.EditManager);
    }

    private bool CanAddManager()
    {
        return _appState.CurrentUser!.Role == ManagerRoleEnum.Admin;
    }

    private void EditManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditManager,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            Branch.Managers.Remove(record);
        }
    }

    private async void Save()
    {
        try
        {
            var branch = await _store.Branches
                .Include(p => p.Cars)
                .Include(p => p.Clients)
                .Include(p => p.Managers)
                .FirstOrDefaultAsync(p => p.Id == Branch.Id);

            branch ??= new Branch();

            _branchMapper.Map(Branch, branch);

            branch.Cars = _store.Cars.Where(p => Branch.Cars.Select(r => r.Id).Contains(p.Id)).ToList();
            branch.Clients = _store.Clients.Where(p => Branch.Clients.Select(r => r.Id).Contains(p.Id)).ToList();
            branch.Managers = _store.Managers.Where(p => Branch.Managers.Select(r => r.Id).Contains(p.Id)).ToList();

            await _store.SaveChangesAsync();

            _notificationService.ShowTip("Обновление филиала", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteBranch()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление филиала",
                "Вы действительно хотите удалить филиал?");

        if (result)
        {
            var branch = await _store.Branches.FirstOrDefaultAsync(p => p.Id == Branch.Id);

            Guard.NotNull(branch, "Не найден филиал");

            _store.Branches.Remove(branch!);

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteBranch()
    {
        return Branch.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public async Task UpdateState(int? entityId = null)
    {
        if (entityId == null)
        {
            Branch = new BranchDto();
            return;
        }

        var branch = await _store.Branches
            .Include(p => p.Cars)
            .Include(p => p.Managers)
            .Include(p => p.Clients)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(branch, "Филиал не найден");

        Branch = _branchMapper.Map(branch!);
    }

    public void SetGrids(SfDataGrid carsDataGrid,
        SfDataGrid clientsDataGrid,
        SfDataGrid managersDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Cars", carsDataGrid },
            { "Clients", clientsDataGrid },
            { "Managers", managersDataGrid },
        };
    }
}