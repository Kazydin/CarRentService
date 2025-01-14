using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Cars.CarsTable;

public sealed partial class CarsTablePage : NavigationPage
{
    public CarsTableViewModel ViewModel { get; }

    public CarsTablePage(CarsTableViewModel viewModel) : base(PageTypeEnum.Cars, "Автомобили")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void CarsTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(CarsDataGrid);
    }
}