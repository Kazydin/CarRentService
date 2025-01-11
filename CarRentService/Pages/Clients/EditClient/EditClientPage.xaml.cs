using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;

namespace CarRentService.Pages.Clients.EditClient;

[InjectDI]
public sealed partial class EditClientPage : NavigationPage
{
    private readonly EditClientViewModel _viewModel;

    public EditClientPage(EditClientViewModel viewModel) : base(PageTypeEnum.EditClient, "�������������� �������")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(object? parameter)
    {
        if (parameter is Client client)
        {
            _viewModel.SetClient(client);
            Header = client.Fio;
        }
        else
        {
            _viewModel.SetClient();
            Header = "�������� �������";
        }
    }
}