using System.Threading.Tasks;
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

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId);
            Header = $"Редактирование платежа № {_viewModel.Payment.Id!.Value}";
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

        Header = "Создание платежа";
    }
}