using System.Threading.Tasks;
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

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        RentalBranch.SelectedIndex = -1;
        _viewModel.SetGrids(CarsDataGrid, InsurancesDataGrid, PaymentsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId);
            Header = $"Редактирование аренды № {_viewModel.Rental.Id!.Value}";
        }
        else
        {
            await _viewModel.UpdateState();
            Header = "Создание аренды";
        }
    }

    private void ViewRentalPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _viewModel.SetXamlRoot(XamlRoot);
    }
}