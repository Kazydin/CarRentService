using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.Pages.Insurances.ViewInsurance;

public sealed partial class ViewInsurancePage : NavigationPage
{
    private readonly ViewInsuranceViewModel _viewModel;

    public ViewInsurancePage(ViewInsuranceViewModel viewModel) : base(PageTypeEnum.EditInsurance)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId);
            Header = $"Редактирование страховки № {_viewModel.Insurance.Id!.Value}";
            return;
        }

        if (parameters is AddRentalPartsNavigationData partsData)
        {
            await _viewModel.InitForRental(partsData.RentalId);
        }
        else
        {
            await _viewModel.UpdateState();
        }

        Header = "Создание страховки";
    }
}