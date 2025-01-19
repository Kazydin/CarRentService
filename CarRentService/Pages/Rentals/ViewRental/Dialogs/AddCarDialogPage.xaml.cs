using AutoMapper;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract.Repositories;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

[InjectDI]
public sealed partial class AddCarDialogPage : Page
{
    public AddCarDialogPage(IMapper mapper, ICarRepository carRepository)
    {
        this.InitializeComponent();
    }
}