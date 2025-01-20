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

namespace CarRentService.Pages.Rentals.RentalsTable;

public partial class RentalsTableViewModel : BaseViewModel
{
    public RelayCommand AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeleteRentalCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<RentalDto> _rentals;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<RentalDto, Rental> _rentalMapper;

    public RentalsTableViewModel(INavigationService navigationService,
        INotificationService notificationService,
        AppDbContext store,
        IUniversalMapper<RentalDto, Rental> rentalMapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _store = store;
        _rentalMapper = rentalMapper;

        // Настройка команд
        AddRentalCommand = new RelayCommand(AddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);
        DeleteRentalCommand = new RelayCommand<object>(DeleteRental);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Rentals = _store.Rentals
            .Select(p => _rentalMapper.Map(p))
            .ToObservableCollection();
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
            var result = await _notificationService.ShowConfirmDialogAsync("Удаление аренды",
                "Вы действительно хотите удалить аренду?");

            if (result)
            {
                var rental = await _store.Rentals.FirstOrDefaultAsync(p => p.Id == record.Id);

                Guard.NotNull(rental, "Не найдена аренда");

                _store.Rentals.Remove(rental!);
                await _store.SaveChangesAsync();

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