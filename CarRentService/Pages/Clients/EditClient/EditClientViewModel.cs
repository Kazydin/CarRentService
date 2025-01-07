using CarRentService.Common.Abstract;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Pages.Clients.EditClient;

public partial class EditClientViewModel : IViewModel
{
    [ObservableProperty]
    private Client _client;
}