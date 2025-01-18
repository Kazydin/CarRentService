using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;

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
            Header = $"Редактирование аренды № {_viewModel.Rental.Id!.Value}";
        }
        else
        {
            _viewModel.SetRental();
            Header = "Создание аренды";
        }
    }
}