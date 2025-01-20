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

namespace CarRentService.Pages.Payments.PaymentsTable;

public partial class PaymentsTableViewModel : BaseViewModel
{
    public RelayCommand AddPaymentCommand { get; }

    public RelayCommand<object> EditPaymentCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeletePaymentCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<PaymentDto> _payments;

    private readonly AppDbContext _store;

    private readonly INavigationService _navigationService;

    private readonly IUniversalMapper<PaymentDto, Payment> _paymentMapper;

    public PaymentsTableViewModel(INavigationService navigationService,
        AppDbContext store,
        IUniversalMapper<PaymentDto, Payment> paymentMapper)
    {
        _navigationService = navigationService;
        _store = store;
        _paymentMapper = paymentMapper;

        // Настройка команд
        AddPaymentCommand = new RelayCommand(AddPayment);
        EditPaymentCommand = new RelayCommand<object>(EditPayment);
        EditRentalCommand = new RelayCommand<object>(EditRental);
        DeletePaymentCommand = new RelayCommand<object>(DeletePayment);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Payments = _store.Payments
            .Select(p => _paymentMapper.Map(p))
            .ToObservableCollection();
    }

    private void AddPayment()
    {
        _navigationService.Navigate(PageTypeEnum.EditPayment);
    }

    private void EditPayment(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditPayment,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void EditRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditRental,
                parameters: new CommonNavigationData(record.Rental!.Id!.Value));
        }
    }

    private async void DeletePayment(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            var payment = await _store.Payments.FirstOrDefaultAsync(p => p.Id == record.Id);

            Guard.NotNull(payment, "Не найден платеж");

            _store.Payments.Remove(payment!);

            await _store.SaveChangesAsync();

            UpdateState();
        }
    }

    public void SetGrids(SfDataGrid paymentsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Payments", paymentsDataGrid }
        };
    }
}