using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Pages.Payments.ViewPayment;

public partial class ViewPaymentViewModel : BaseViewModel
{
    public RelayCommand DeletePaymentCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private PaymentDto _payment;

    [ObservableProperty] private ObservableCollection<RentalDto> _rentals;

    [ObservableProperty] private ObservableCollection<PaymentMethodEnum> _methods;

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

        Methods = EnumExtensions.GetValues<PaymentMethodEnum>().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            var payment = await _store.Payments.FirstOrDefaultAsync(p => p.Id == Payment.Id) ?? new Payment();

            _paymentMapper.Map(Payment, payment);

            if (Payment.Rental != null)
            {
                payment.Rental = await _store.Rentals.SingleAsync(p => p.Id == Payment.Rental.Id);
            }

            _paymentMapper.Validate(payment);

            if (payment.Id == 0)
            {
                _store.Payments.Add(payment);
            }

            await _store.SaveChangesAsync();

            await UpdateState(payment.Id);

            _notificationService.ShowTip("Обновление платежа", "Сохранено успешно!");

            _navigationService.GoBack();
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

    public async Task InitForRental(int rentalId)
    {
        Payment = null;

        SetRentals();

        Payment = new PaymentDto
        {
            Date = DateTime.Now
        };

        var rental = await _store.Rentals.SingleAsync(p => p.Id == rentalId);

        Payment.Rental = _rentalMapper.Map(rental);
    }

    private void SetRentals()
    {
        Rentals = _store.Rentals
            .Where(p => p.Status != RentalStatusEnum.Completed)
            .Include(p => p.Client)
            .Select(p => _rentalMapper.Map(p))
            .ToObservableCollection();
    }

    public async Task UpdateState(int? entityId = null)
    {
        Payment = null;

        SetRentals();

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