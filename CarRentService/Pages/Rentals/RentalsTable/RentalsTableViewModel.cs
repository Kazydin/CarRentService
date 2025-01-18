using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;

namespace CarRentService.Pages.Rentals.RentalsTable;

public partial class RentalsTableViewModel : BaseViewModel
{
    public RelayCommand AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeleteRentalCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<RentalDto> _rentals;

    private readonly IRentalService _rentalService;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    public RentalsTableViewModel(IRentalService rentalService,
        INavigationService navigationService,
        INotificationService notificationService)
    {
        _rentalService = rentalService;
        _navigationService = navigationService;
        _notificationService = notificationService;

        // Настройка команд
        AddRentalCommand = new RelayCommand(AddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);
        DeleteRentalCommand = new RelayCommand<object>(DeleteRental);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Rentals = _rentalService.GetDtos();
    }

    private void AddRental()
    {
        _navigationService.Navigate(PageTypeEnum.EditRental);
    }

    private void EditRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is RentalDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditRental,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is RentalDto record)
        {
            var result = await _notificationService.ShowConfirmDialogAsync($"Удаление аренды №{record.Id!.Value}",
                "Вы действительно хотите удалить аренду?");

            if (result)
            {
                _rentalService.Remove(record.Id!.Value);
                UpdateState();
            }
        }
    }

    public void SetGrids(SfDataGrid rentalsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Rentals", rentalsDataGrid }
        };
    }
}