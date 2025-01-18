using System.Collections.Generic;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;
using System.ComponentModel;
using System;

namespace CarRentService.Pages.Manages.ViewManager;

public partial class ViewManagerViewModel : BaseViewModel
{
    public RelayCommand DeleteManagerCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    [ObservableProperty] private ObservableCollection<Branch> _branches;

    [ObservableProperty] private ObservableCollection<string> _roles;

    [ObservableProperty] private ObservableCollection<Branch> _selectedBranches = new();

    [ObservableProperty]
    private bool _isBranchesEnabled;

    private readonly INavigationService _navigationService;

    private readonly IManagerService _managerService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    private IList<object>? _selectedBranchesComboBox;

    public ViewManagerViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IManagerService managerService,
        IMapper mapper,
        IBranchService branchService)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _managerService = managerService;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteManagerCommand = new RelayCommand(DeleteManager, CanDeleteManager);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        Branches = branchService.Table;
        Roles = typeof(ManagerRoleEnum).GetDescriptions().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            Manager.Branches = SelectedBranches;

            var d = _mapper.Map<Manager>(Manager);

            _managerService.Update(d);

            _notificationService.ShowTip("Обновление менеджера", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteManager()
    {
        Guard.NotNull(Manager, "Нельзя удалить менеджера, который еще не сохранен");

        _managerService.Remove(Manager.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteManager()
    {
        return Manager.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetManager(int? entityId = null, IList<object>? selectedBranches = null)
    {
        if (entityId == null)
        {
            Manager = new ManagerDto();
            return;
        }

        Manager = _managerService.GetDto(entityId.Value);
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
        IsBranchesEnabled = Manager.Role == ManagerRoleEnum.BranchManager.GetDescription();

        if (Manager.Role == ManagerRoleEnum.BranchManager.GetDescription())
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