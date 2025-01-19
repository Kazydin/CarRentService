using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;

namespace CarRentService.Pages.Manages.ViewManager;

public partial class ViewManagerViewModel : BaseViewModel
{
    public RelayCommand DeleteManagerCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    [ObservableProperty] private ObservableCollection<Branch> _branches;

    [ObservableProperty] private ObservableCollection<ManagerRoleEnum> _roles;

    [ObservableProperty] private ObservableCollection<Branch> _selectedBranches = new();

    [ObservableProperty]
    private bool _isBranchesEnabled;

    private readonly INavigationService _navigationService;

    private readonly IManagerRepository _managerRepository;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    private IList<object>? _selectedBranchesComboBox;

    public ViewManagerViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IManagerRepository managerRepository,
        IMapper mapper,
        IBranchRepository branchRepository)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _managerRepository = managerRepository;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteManagerCommand = new RelayCommand(DeleteManager, CanDeleteManager);

        Branches = branchRepository.Table;
        Roles = EnumExtensions.GetValues<ManagerRoleEnum>().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            Manager.Branches = _mapper.Map<ObservableCollection<BranchDto>>(SelectedBranches);

            _managerRepository.Update(_mapper.Map<Manager>(Manager));

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

        _managerRepository.Remove(Manager.Id!.Value);
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

        Manager = _managerRepository.GetDto(entityId.Value);
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