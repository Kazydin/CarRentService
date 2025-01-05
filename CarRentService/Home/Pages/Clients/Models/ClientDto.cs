using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Home.Pages.Clients.Models
{
    public partial class ClientDto : Client
    {
        [ObservableProperty]
        private bool _isSelected = true;
    }
}
