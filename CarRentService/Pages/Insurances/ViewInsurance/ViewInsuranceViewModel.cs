using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Pages.Insurances.ViewInsurance;

public partial class ViewInsuranceViewModel : BaseViewModel
{
    public RelayCommand DeleteInsuranceCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private InsuranceDto _insurance;

    [ObservableProperty] private ObservableCollection<InsuranceTypeEnum> _types;

    [ObservableProperty] private ObservableCollection<RentalDto> _rentals;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<InsuranceDto, Insurance> _insuranceMapper;

    private readonly IUniversalMapper<RentalDto, Rental> _rentalMapper;

    private readonly AppDbContext _store;

    public ViewInsuranceViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IUniversalMapper<InsuranceDto, Insurance> insuranceMapper,
        AppDbContext store,
        IUniversalMapper<RentalDto, Rental> rentalMapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _insuranceMapper = insuranceMapper;
        _store = store;
        _rentalMapper = rentalMapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteInsuranceCommand = new RelayCommand(DeleteInsurance, CanDeleteInsurance);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        Types = EnumExtensions.GetValues<InsuranceTypeEnum>().ToObservableCollection();
    }

    private async void Save()
    {
        try
        {
            var insurance = await _store.Insurances.FirstOrDefaultAsync(p => p.Id == Insurance.Id) ?? new Insurance();

            _insuranceMapper.Map(Insurance, insurance);

            if (Insurance.Rental != null)
            {
                insurance.Rental = await _store.Rentals.SingleAsync(p => p.Id == Insurance.Rental.Id);
            }

            _insuranceMapper.Validate(insurance);

            if (insurance.Id == 0)
            {
                _store.Insurances.Add(insurance);
            }

            await _store.SaveChangesAsync();

            await UpdateState(insurance.Id);

            _notificationService.ShowTip("Обновление страхования", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (Exception e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteInsurance()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление страхования",
                "Вы действительно хотите удалить страхование?");

        if (result)
        {
            var insurance = await _store.Insurances.SingleAsync(p => p.Id == Insurance.Id);

            _store.Insurances.Remove(insurance);

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteInsurance()
    {
        return Insurance.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public async Task InitForRental(int rentalId)
    {
        Insurance = null;

        SetRentals();

        Insurance = new InsuranceDto();

        var rental = await _store.Rentals.SingleAsync(p => p.Id == rentalId);

        Insurance.Rental = _rentalMapper.Map(rental);
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
        Insurance = null;
        SetRentals();

        if (entityId == null)
        {
            Insurance = new InsuranceDto();
            return;
        }

        var insurance = await _store.Insurances
            .Include(p => p.Car)
            .Include(p => p.Rental)
            .ThenInclude(p => p.Cars)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(insurance, "Страхование не найдено");

        Insurance = _insuranceMapper.Map(insurance!);
    }
}