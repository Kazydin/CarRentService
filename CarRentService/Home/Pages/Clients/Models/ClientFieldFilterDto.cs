using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Home.Pages.Clients.Models
{
    [ObservableObject]
    public partial class ClientDto
    {
        [ObservableProperty]
        private Client _client;

        [ObservableProperty]
        private bool _isSelected = true;

        public ClientDto(Client client)
        {
            _client = client;
        }
    }
}
