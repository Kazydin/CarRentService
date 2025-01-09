using System.Runtime.CompilerServices;

using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;

using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Clients.EditClient;

[InjectDI]
public sealed partial class EditClientPage : NavigationPage
{
    private readonly EditClientViewModel _viewModel;

    public EditClientPage(EditClientViewModel viewModel) : base(PageTypeEnum.EditClient, "Редактирование клиента")
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
    }
}