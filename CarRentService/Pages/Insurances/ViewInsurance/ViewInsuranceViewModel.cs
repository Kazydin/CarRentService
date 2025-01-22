using System;
using System.Collections.ObjectModel;
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

namespace CarRentService.Pages.Insurances.ViewInsurance;

public partial class ViewInsuranceViewModel : BaseViewModel
{
    public RelayCommand DeleteInsuranceCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private InsuranceDto _insurance;

    [ObservableProperty] private ObservableCollection<InsuranceTypeEnum> _types;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<InsuranceDto, Insurance> _insuranceMapper;

    private readonly AppDbContext _store;

    public ViewInsuranceViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IUniversalMapper<InsuranceDto, Insurance> insuranceMapper,
        AppDbContext store)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _insuranceMapper = insuranceMapper;
        _store = store;

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
            var insurance = await _store.Insurances.FirstOrDefaultAsync(p => p.Id == Insurance.Id);

            Guard.NotNull(insurance, "Не найдена страхование");

            _insuranceMapper.Map(Insurance, insurance!);

            await _store.SaveChangesAsync();

            await UpdateState(insurance!.Id);

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
            var insurance = await _store.Insurances.FirstOrDefaultAsync(p => p.Id == Insurance.Id);

            Guard.NotNull(insurance, "Не найдено страхование");

            _store.Insurances.Remove(insurance!);

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

    public async Task UpdateState(int? entityId = null)
    {
        if (entityId == null)
        {
            Insurance = new InsuranceDto();
            return;
        }

        var insurance = await _store.Insurances.FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(insurance, "Страхование не найдено");

        Insurance = _insuranceMapper.Map(insurance!);
    }
}