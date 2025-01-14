using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;

namespace CarRentService.Pages.Cars.ViewCars;

public sealed partial class ViewCarPage : NavigationPage
{
    private readonly ViewCarViewModel _viewModel;

    public ViewCarPage(ViewCarViewModel viewModel) : base(PageTypeEnum.EditCar, "Редактирование автоиобиля")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(RentalsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetCar(data.EntityId);
            Header = data.Header;
        }
        else
        {
            _viewModel.SetCar();
            Header = "Создание автомобиля";
        }
    }
}