using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;
using CarRentService.Common.Models;
using CarRentService.Common;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Insurances.ViewInsurance;

public partial class ViewInsuranceViewModel : BaseViewModel
{
    public RelayCommand DeleteInsuranceCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private InsuranceDto _insurance;

    [ObservableProperty] private ObservableCollection<InsuranceTypeEnum> _types;

    private readonly INavigationService _navigationService;

    private readonly IInsuranceService _insuranceService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewInsuranceViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IInsuranceService insuranceService,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _insuranceService = insuranceService;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteInsuranceCommand = new RelayCommand(DeleteInsurance, CanDeleteInsurance);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        EditRentalCommand = new RelayCommand<object>(EditRental);

        Types = EnumExtensions.GetValues<InsuranceTypeEnum>().ToObservableCollection();
    }

    private void EditRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditRental, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void Save()
    {
        try
        {
            _insuranceService.Update(_mapper.Map<Insurance>(Insurance));

            _notificationService.ShowTip("Обновление страхования", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteInsurance()
    {
        Guard.NotNull(Insurance, "Нельзя удалить страхование, которое еще не сохранено");

        _insuranceService.Remove(Insurance.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteInsurance()
    {
        return Insurance.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetInsurance(int? entityId = null)
    {
        if (entityId == null)
        {
            Insurance = new InsuranceDto();
            return;
        }

        Insurance = _insuranceService.GetDto(entityId.Value);
    }
}