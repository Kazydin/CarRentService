using System;
using System.Buffers;
using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using System.ComponentModel.DataAnnotations;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;

namespace CarRentService.Pages.Payments.ViewPayment;

public partial class ViewPaymentViewModel : BaseViewModel
{
    public RelayCommand DeletePaymentCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand EditRentalCommand { get; }

    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private PaymentDto _payment;

    [ObservableProperty] private readonly ObservableCollection<string> _methods;

    private readonly INavigationService _navigationService;

    private readonly IPaymentService _paymentService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewPaymentViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IPaymentService paymentService,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _paymentService = paymentService;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeletePaymentCommand = new RelayCommand(DeletePayment, CanDeletePayment);
        EditRentalCommand = new RelayCommand(EditRental);

        Methods = typeof(PaymentMethodEnum).GetDescriptions().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            _paymentService.Update(_mapper.Map<Payment>(Payment));

            _notificationService.ShowTip("Обновление платежа", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeletePayment()
    {
        Guard.NotNull(Payment, "Нельзя удалить платеж, который еще не сохранен");

        _paymentService.Remove(Payment.Id!.Value);
        _navigationService.GoBack();
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
        throw new NotImplementedException();
    }

    public void SetPayment(int? entityId = null)
    {
        if (entityId == null)
        {
            Payment = new PaymentDto();
            return;
        }

        Payment = _paymentService.GetDto(entityId.Value);
    }
}