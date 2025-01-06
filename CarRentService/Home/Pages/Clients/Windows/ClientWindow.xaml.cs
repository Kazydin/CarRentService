using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;

namespace CarRentService.Home.Pages.Clients.Windows
{
    public sealed partial class ClientWindow : InjectedWindow
    {
        public ClientWindow() : base(WindowTypeEnum.Client)
        {
            this.InitializeComponent();

            
        }
    }
}
