using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;

namespace CarRentService.Pages.Cars.ViewCars;

public sealed partial class ViewCarPage : NavigationPage
{
    private readonly ViewCarViewModel _viewModel;

    public ViewCarPage(ViewCarViewModel viewModel) : base(PageTypeEnum.EditCar)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(RentalsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId);
            Header = $"Редактирование автомобиля № {_viewModel.Car.Id!.Value}";
        }
        else
        {
            await _viewModel.UpdateState();
            Header = "Создание автомобиля";
        }
    }
}