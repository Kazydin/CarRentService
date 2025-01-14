using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;

namespace CarRentService.Pages.Clients.ViewClient;

public sealed partial class ViewClientPage : NavigationPage
{
    private readonly ViewClientViewModel _viewModel;

    public ViewClientPage(ViewClientViewModel viewModel) : base(PageTypeEnum.EditClient, "Редактирование клиента")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(RentalsDataGrid, CarsDataGrid, InsurancesDataGrid, PaymentsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetClient(data.EntityId);
            Header = data.Header;
        }
        else
        {
            _viewModel.SetClient();
            Header = "Создание клиента";
        }
    }
}