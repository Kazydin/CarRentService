using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;
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

    private readonly INavigationService _navigationService;

    private readonly IUniversalMapper<InsuranceDto, Insurance> _insuranceMapper;

    private readonly AppDbContext _store;

    public InsurancesTableViewModel(INavigationService navigationService,
        IUniversalMapper<InsuranceDto, Insurance> insuranceMapper,
        AppDbContext store)
    {
        _navigationService = navigationService;
        _insuranceMapper = insuranceMapper;
        _store = store;

        // Настройка команд
        AddInsuranceCommand = new RelayCommand(AddInsurance);
        EditInsuranceCommand = new RelayCommand<object>(EditInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(DeleteInsurance);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Insurances = _store.Insurances
            .Select(p => _insuranceMapper.Map(p))
            .ToObservableCollection();
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

    private async void DeleteInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            var insurance = await _store.Insurances.FirstOrDefaultAsync(p => p.Id == record.Id);

            Guard.NotNull(insurance, "Не найдена страховка");

            _store.Insurances.Remove(insurance!);

            await _store.SaveChangesAsync();

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