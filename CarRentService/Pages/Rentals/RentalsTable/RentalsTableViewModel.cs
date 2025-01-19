using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Rentals.RentalsTable;

public partial class RentalsTableViewModel : BaseViewModel
{
    public RelayCommand AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeleteRentalCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<RentalDto> _rentals;

    private readonly IRentalRepository _rentalRepository;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    public RentalsTableViewModel(IRentalRepository rentalRepository,
        INavigationService navigationService,
        INotificationService notificationService)
    {
        _rentalRepository = rentalRepository;
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
        Rentals = _rentalRepository.GetDtos();
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
                _rentalRepository.Remove(record.Id!.Value);
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