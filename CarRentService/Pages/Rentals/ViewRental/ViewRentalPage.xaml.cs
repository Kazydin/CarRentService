using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Rentals.ViewRental;

public sealed partial class ViewRentalPage : NavigationPage
{
    private readonly ViewRentalViewModel _viewModel;

    public ViewRentalPage(ViewRentalViewModel viewModel) : base(PageTypeEnum.EditRental)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(CarsDataGrid, InsurancesDataGrid, PaymentsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetRental(data.EntityId);
            Header = $"�������������� ������ � {_viewModel.Rental.Id!.Value}";
        }
        else
        {
            _viewModel.SetRental();
            Header = "�������� ������";
        }
    }

    private void ViewRentalPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _viewModel.SetXamlRoot(XamlRoot);
    }
}