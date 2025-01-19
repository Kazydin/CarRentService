using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Insurances.InsurancesTable;

public partial class InsurancesTableViewModel : BaseViewModel
{
    public RelayCommand AddInsuranceCommand { get; }

    public RelayCommand<object> EditInsuranceCommand { get; }

    public RelayCommand<object> DeleteInsuranceCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<InsuranceDto> _insurances;

    private readonly IInsuranceRepository _insuranceRepository;

    private readonly INavigationService _navigationService;

    public InsurancesTableViewModel(IInsuranceRepository insuranceRepository,
        INavigationService navigationService)
    {
        _insuranceRepository = insuranceRepository;
        _navigationService = navigationService;

        // Настройка команд
        AddInsuranceCommand = new RelayCommand(AddInsurance);
        EditInsuranceCommand = new RelayCommand<object>(EditInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(DeleteInsurance);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Insurances = _insuranceRepository.GetDtos();
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
            _insuranceRepository.Remove(record.Id);
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