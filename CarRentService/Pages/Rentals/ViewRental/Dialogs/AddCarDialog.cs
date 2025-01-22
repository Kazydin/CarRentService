using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

[InjectDI]
[ObservableObject]
public partial class AddCarDialog
{
    private readonly ContentDialog _dialog;

    private readonly AddCarDialogViewModel _viewModel;

    public AddCarDialog(AddCarDialogPage dialogPage,
        AddCarDialogViewModel viewModel)
    {
        _viewModel = viewModel;
        dialogPage.DataContext = viewModel;

        _dialog = new ContentDialog
        {
            Title = "Добавление автомобиля",
            Content = dialogPage,
            PrimaryButtonText = "Добавить",
            CloseButtonText = "Отмена",
        };

        _dialog.PrimaryButtonClick += _dialog_PrimaryButtonClick;
    }

    private void _dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (_viewModel.AddCarCommand.CanExecute(null))
        {
            _viewModel.AddCarCommand.Execute(null);
        }

        if (!_viewModel.CanExit)
        {
            args.Cancel = true;
        }
    }

    public async Task<CarDto> ShowAsync(RentalDto rental, XamlRoot xamlRoot)
    {
        _viewModel.OnShow(rental);

        _dialog.XamlRoot = xamlRoot;
        await _dialog.ShowAsync();

        return _viewModel.Car;
    }

}