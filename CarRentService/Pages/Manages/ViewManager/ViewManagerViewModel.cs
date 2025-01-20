using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Pages.Manages.ViewManager;

public partial class ViewManagerViewModel : BaseViewModel
{
    public RelayCommand DeleteManagerCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    [ObservableProperty] private ObservableCollection<BranchDto> _branches;

    [ObservableProperty] private ObservableCollection<ManagerRoleEnum> _roles;

    [ObservableProperty] private ObservableCollection<BranchDto> _selectedBranches = new();

    [ObservableProperty]
    private bool _isBranchesEnabled;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private IList<object>? _selectedBranchesComboBox;

    private readonly IUniversalMapper<ManagerDto, Manager> _managerMapper;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly AppDbContext _store;

    public ViewManagerViewModel(INavigationService navigationService,
        INotificationService notificationService,
        AppDbContext store,
        IUniversalMapper<ManagerDto, Manager> managerMapper,
        IUniversalMapper<BranchDto, Branch> branchMapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _store = store;
        _managerMapper = managerMapper;
        _branchMapper = branchMapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteManagerCommand = new RelayCommand(DeleteManager, CanDeleteManager);

        Roles = EnumExtensions.GetValues<ManagerRoleEnum>().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            var manager = await _store.Managers
                .Include(p => p.Branches)
                .SingleAsync(p => p.Id == Manager.Id);

            Manager.Branches = SelectedBranches;

            _managerMapper.Map(Manager, manager!);

            manager.Branches = _store.Branches
                .Where(p => SelectedBranches.Select(r => r.Id).Contains(p.Id))
                .ToList();

            await _store.SaveChangesAsync();

            _notificationService.ShowTip("Обновление менеджера", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteManager()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление менеджера",
                "Вы действительно хотите удалить менеджера?");

        if (result)
        {
            var manager = await _store.Managers
                .Include(p => p.Branches)
                .FirstOrDefaultAsync(p => p.Id == Manager.Id);

            Guard.NotNull(manager, "Не найден менеджер");

            _store.Managers.Remove(manager!);

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteManager()
    {
        return Manager.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public async Task UpdateState(int? entityId = null, IList<object>? selectedBranches = null)
    {
        Branches = _store.Branches
            .Select(p => _branchMapper.Map(p))
            .ToObservableCollection();

        if (entityId == null)
        {
            Manager = new ManagerDto();
            return;
        }

        var manager = await _store.Managers
            .Include(p => p.Branches)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Manager = _managerMapper.Map(manager!);

        UpdateStateFromManager();

        if (selectedBranches == null)
        {
            return;
        }

        _selectedBranchesComboBox = selectedBranches;

        selectedBranches.Clear();
        foreach (var managerBranch in Manager.Branches)
        {
            selectedBranches.Add(Branches.First(p => p.Id == managerBranch.Id));
        }
    }

    partial void OnManagerChanged(ManagerDto oldValue, ManagerDto newValue)
    {
        // Отписываемся от старого объекта, если он был
        if (oldValue != null)
        {
            oldValue.PropertyChanged -= OnManagerPropertyChanged;
        }

        // Подписываемся на новый объект
        if (newValue != null)
        {
            newValue.PropertyChanged += OnManagerPropertyChanged;
        }
    }

    private void OnManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Manager.Role))
        {
            UpdateStateFromManager();
        }
    }

    private void UpdateStateFromManager()
    {
        if (Manager.Role == ManagerRoleEnum.BranchManager)
        {
            IsBranchesEnabled = true;
        }
        else
        {
            _selectedBranchesComboBox?.Clear();
            IsBranchesEnabled = false;
        }
    }
}