using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;

namespace CarRentService.Pages.Insurances.InsurancesTable;

public partial class InsurancesTableViewModel : BaseViewModel
{
    public RelayCommand AddInsuranceCommand { get; }

    public RelayCommand<object> EditInsuranceCommand { get; }

    public RelayCommand<object> DeleteInsuranceCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<InsuranceDto> _insurances;

    private readonly IInsuranceService _insuranceService;

    private readonly INavigationService _navigationService;

    public InsurancesTableViewModel(IInsuranceService insuranceService,
        INavigationService navigationService)
    {
        _insuranceService = insuranceService;
        _navigationService = navigationService;

        // Настройка команд
        AddInsuranceCommand = new RelayCommand(AddInsurance);
        EditInsuranceCommand = new RelayCommand<object>(EditInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(DeleteInsurance);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Insurances = _insuranceService.GetDtos();
    }

    private void AddInsurance()
    {
        _navigationService.Navigate(PageTypeEnum.EditInsurance);
    }

    private void EditInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditInsurance, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void DeleteInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Insurance record)
        {
            _insuranceService.Remove(record.Id);
            UpdateState();
        }
    }

    public void SetGrids(SfDataGrid insurancesDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Insurances", insurancesDataGrid }
        };
    }
}