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

namespace CarRentService.Pages.Payments.PaymentsTable;

public partial class PaymentsTableViewModel : BaseViewModel
{
    public RelayCommand AddPaymentCommand { get; }

    public RelayCommand<object> EditPaymentCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeletePaymentCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<PaymentDto> _payments;

    private readonly IPaymentRepository _paymentRepository;

    private readonly INavigationService _navigationService;

    public PaymentsTableViewModel(IPaymentRepository paymentRepository,
        INavigationService navigationService)
    {
        _paymentRepository = paymentRepository;
        _navigationService = navigationService;

        // Настройка команд
        AddPaymentCommand = new RelayCommand(AddPayment);
        EditPaymentCommand = new RelayCommand<object>(EditPayment);
        EditRentalCommand = new RelayCommand<object>(EditRental);
        DeletePaymentCommand = new RelayCommand<object>(DeletePayment);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Payments = _paymentRepository.GetDtos();
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

    private void DeletePayment(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            _paymentRepository.Remove(record.Id!.Value);
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