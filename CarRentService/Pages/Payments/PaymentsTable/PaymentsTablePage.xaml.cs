using System.Data;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Payments.PaymentsTable;

public sealed partial class PaymentsTablePage : NavigationPage
{
    public PaymentsTableViewModel ViewModel { get; }

    public PaymentsTablePage(PaymentsTableViewModel viewModel) : base(PageTypeEnum.Payments, "Платежи")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void PaymentsTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(PaymentsDataGrid);
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        ViewModel.UpdateState();
    }
}