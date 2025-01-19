using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Insurances.InsurancesTable;

public sealed partial class InsurancesTablePage : NavigationPage
{
    public InsurancesTableViewModel ViewModel { get; }

    public InsurancesTablePage(InsurancesTableViewModel viewModel) : base(PageTypeEnum.Insurances, "Страховки")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void InsurancesTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(InsurancesDataGrid);
    }
}