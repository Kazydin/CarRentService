using CarRentService.Common.Attributes;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

[InjectDI]
public sealed partial class AddCarDialogPage : Page
{
    public AddCarDialogPage()
    {
        this.InitializeComponent();
    }
}