using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Branches.BranchesTable;

public partial class BranchesTableViewModel : BaseViewModel
{
    public RelayCommand AddBranchCommand { get; }

    public RelayCommand<object> EditBranchCommand { get; }

    public RelayCommand<object> DeleteBranchCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<BranchDto> _branches;

    private readonly IBranchRepository _branchRepository;

    private readonly INavigationService _navigationService;

    private readonly IMapper _mapper;

    public BranchesTableViewModel(IBranchRepository branchRepository,
        INavigationService navigationService,
        IMapper mapper)
    {
        _branchRepository = branchRepository;
        _navigationService = navigationService;
        _mapper = mapper;

        // Настройка команд
        AddBranchCommand = new RelayCommand(AddBranch);
        EditBranchCommand = new RelayCommand<object>(EditBranch);
        DeleteBranchCommand = new RelayCommand<object>(DeleteBranch);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Branches = _branchRepository.GetDtos();
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

    private void DeleteBranch(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is BranchDto record)
        {
            _branchRepository.Remove(record.Id!.Value);
            UpdateState();
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