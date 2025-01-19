using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;

namespace CarRentService.Pages.Payments.ViewPayment;

public sealed partial class ViewPaymentPage : NavigationPage
{
    private readonly ViewPaymentViewModel _viewModel;

    public ViewPaymentPage(ViewPaymentViewModel viewModel) : base(PageTypeEnum.EditPayment)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetPayment(data.EntityId);
            Header = $"Редактирование платежа № {_viewModel.Payment.Id!.Value}";
        }
        else
        {
            _viewModel.SetPayment();
            Header = "Создание платежа";
        }
    }
}