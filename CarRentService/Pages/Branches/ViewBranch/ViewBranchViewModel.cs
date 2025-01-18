﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
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

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private BranchDto _branch;

    private readonly INavigationService _navigationService;

    private readonly IBranchService _branchService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewBranchViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IBranchService branchService,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _branchService = branchService;
        _notificationService = notificationService;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteBranchCommand = new RelayCommand(DeleteBranch, CanDeleteBranch);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddCarCommand = new RelayCommand(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);

        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);

        AddManagerCommand = new RelayCommand(AddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
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

    private void EditManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditManager, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void Save()
    {
        try
        {
            _branchService.Update(_mapper.Map<Branch>(Branch));

            _notificationService.ShowTip("Обновление филиала", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteBranch()
    {
        Guard.NotNull(Branch, "Нельзя удалить филиал, который еще не сохранен");

        _branchService.Remove(Branch.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteBranch()
    {
        return Branch.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetBranch(int? entityId = null)
    {
        if (entityId == null)
        {
            Branch = new BranchDto();
            return;
        }

        Branch = _branchService.GetDto(entityId.Value);
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