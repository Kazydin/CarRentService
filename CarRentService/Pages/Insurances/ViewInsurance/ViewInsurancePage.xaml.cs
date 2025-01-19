using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;

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

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetInsurance(data.EntityId);
            Header = $"Редактирование страховки № {_viewModel.Insurance.Id}";
        }
        else
        {
            _viewModel.SetInsurance();
            Header = "Создание страховки";
        }
    }
}