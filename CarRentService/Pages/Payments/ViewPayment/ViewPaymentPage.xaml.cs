using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;

namespace CarRentService.Pages.Payments.ViewPayment;

public sealed partial class ViewPaymentPage : NavigationPage
{
    private readonly ViewPaymentViewModel _viewModel;

    public ViewPaymentPage(ViewPaymentViewModel viewModel) : base(PageTypeEnum.EditPayment, "Редактирование платежа")
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
            Header = data.Header;
        }
        else
        {
            _viewModel.SetPayment();
            Header = "Создание платежа";
        }
    }
}