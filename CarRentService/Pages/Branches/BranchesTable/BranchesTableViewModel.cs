﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Branches.BranchesTable;

public partial class BranchesTableViewModel : BaseViewModel
{
    public RelayCommand AddBranchCommand { get; }

    public RelayCommand<object> EditBranchCommand { get; }

    public RelayCommand<object> DeleteBranchCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    private readonly INotificationService _notificationService;

    [ObservableProperty]
    private ObservableCollection<BranchDto> _branches;

    private readonly INavigationService _navigationService;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly AppDbContext _store;

    public BranchesTableViewModel(INavigationService navigationService,
        IUniversalMapper<BranchDto, Branch> branchMapper,
        AppDbContext store,
        INotificationService notificationService)
    {
        _navigationService = navigationService;
        _branchMapper = branchMapper;
        _store = store;
        _notificationService = notificationService;

        // Настройка команд
        AddBranchCommand = new RelayCommand(AddBranch);
        EditBranchCommand = new RelayCommand<object>(EditBranch);
        DeleteBranchCommand = new RelayCommand<object>(DeleteBranch);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Branches = _store.Branches
            .Include(p => p.Cars)
            .Include(p => p.Clients)
            .Include(p => p.Managers)
            .Select(p => _branchMapper.Map(p))
            .ToObservableCollection();
    }

    private void AddBranch()
    {
        _navigationService.Navigate(PageTypeEnum.EditBranch);
    }

    private void EditBranch(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is BranchDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditBranch, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteBranch(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is BranchDto record)
        {
            var result = await _notificationService.ShowConfirmDialogAsync("Удаление филиала",
                "Вы действительно хотите удалить филиал?");

            if (result)
            {
                var branch = await _store.Branches
                    .Include(p => p.Cars)
                    .Include(p => p.Clients)
                    .Include(p => p.Managers)
                    .SingleAsync(p => p.Id == record.Id);

                if (branch.Cars.Any())
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "У филиала есть автомобили");
                    return;
                }

                if (branch.Clients.Any())
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "У филиала есть клиенты");
                    return;
                }

                if (branch.Managers.Any())
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "У филиала есть менеджеры");
                    return;
                }

                _store.Branches.Remove(branch!);
                await _store.SaveChangesAsync();

                UpdateState();
            }
        }
    }

    public void SetGrids(SfDataGrid branchesDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Branches", branchesDataGrid }
        };
    }
}