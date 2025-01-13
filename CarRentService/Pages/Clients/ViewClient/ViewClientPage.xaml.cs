using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;

namespace CarRentService.Pages.Clients.ViewClient;

[InjectDI]
public sealed partial class ViewClientPage : NavigationPage
{
    private readonly ViewClientViewModel _viewModel;

    public ViewClientPage(ViewClientViewModel viewModel) : base(PageTypeEnum.EditClient, "Редактирование клиента")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(object? parameter)
    {
        _viewModel.SetGrids(RentalsDataGrid, CarsDataGrid, InsurancesDataGrid, PaymentsDataGrid);

        if (parameter is Client client)
        {
            _viewModel.SetClient(client);
            Header = client.Fio;
        }
        else
        {
            _viewModel.SetClient();
            Header = "Создание клиента";
        }
    }
}