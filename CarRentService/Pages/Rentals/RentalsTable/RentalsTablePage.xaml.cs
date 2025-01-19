using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Rentals.RentalsTable;

public sealed partial class RentalsTablePage : NavigationPage
{
    public RentalsTableViewModel ViewModel { get; }

    public RentalsTablePage(RentalsTableViewModel viewModel) : base(PageTypeEnum.Rentals, "Аренды")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void RentalsTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(RentalsDataGrid);
    }
}