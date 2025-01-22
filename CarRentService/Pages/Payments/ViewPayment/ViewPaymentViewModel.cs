using System;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Windows.Media.Protection.PlayReady;

namespace CarRentService.Pages.Payments.ViewPayment;

public partial class ViewPaymentViewModel : BaseViewModel
{
    public RelayCommand DeletePaymentCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand EditRentalCommand { get; }

    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private PaymentDto _payment;

    [ObservableProperty] private ObservableCollection<string> _methods;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<PaymentDto, Payment> _paymentMapper;

    private readonly IUniversalMapper<RentalDto, Rental> _rentalMapper;

    private readonly AppDbContext _store;

    public ViewPaymentViewModel(INavigationService navigationService,
        INotificationService notificationService,
        AppDbContext store,
        IUniversalMapper<PaymentDto, Payment> paymentMapper,
        IUniversalMapper<RentalDto, Rental> rentalMapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _store = store;
        _paymentMapper = paymentMapper;
        _rentalMapper = rentalMapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeletePaymentCommand = new RelayCommand(DeletePayment, CanDeletePayment);
        EditRentalCommand = new RelayCommand(EditRental);

        Methods = typeof(PaymentMethodEnum)
            .GetDescriptions()
            .ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            var payment = await _store.Payments.FirstOrDefaultAsync(p => p.Id == Payment.Id) ?? new Payment();

            _paymentMapper.Map(Payment, payment);

            payment.Rental = await _store.Rentals.SingleAsync(p => p.Id == Payment.Rental.Id);

            _paymentMapper.Validate(payment);

            if (payment.Id == 0)
            {
                _store.Payments.Add(payment);
            }

            await _store.SaveChangesAsync();

            var d = await _store.Payments.ToListAsync();
            
            await UpdateState(payment.Id);

            _notificationService.ShowTip("Обновление платежа", "Сохранено успешно!");
        }
        catch (Exception e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeletePayment()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление платежа",
                "Вы действительно хотите удалить платеж");

        if (result)
        {
            var payment = await _store.Payments.SingleAsync(p => p.Id == Payment.Id);

            _store.Payments.Remove(payment);

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeletePayment()
    {
        return Payment.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    private void EditRental()
    {
        _navigationService.Navigate(PageTypeEnum.EditRental,
            parameters: new CommonNavigationData(Payment.Rental!.Id!.Value));
    }

    public async Task InitForRental(int rentalId)
    {
        Payment = new PaymentDto
        {
            Date = DateTime.Now
        };

        var rental = await _store.Rentals.SingleAsync(p => p.Id == rentalId);

        Payment.Rental = _rentalMapper.Map(rental);
    }

    public async Task UpdateState(int? entityId = null)
    {
        if (entityId == null)
        {
            Payment = new PaymentDto
            {
                Date = DateTime.Now
            };
            return;
        }

        var payment = await _store.Payments
            .Include(p => p.Rental)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(payment, "Платеж не найдена");

        Payment = _paymentMapper.Map(payment!);
    }
}