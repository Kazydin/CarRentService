using System;
using CarRentService.Login;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace CarRentService.Home;

public sealed partial class HomePage : Page
{
    private IServiceProvider _serviceProvider;

    public HomePage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        this.InitializeComponent();

        this.Loaded += HomePage_Loaded;
    }

    private async void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        // Ожидаем, пока окно отобразится
        var dialog = new ContentDialog
        {
            Title = "Авторизация",
            PrimaryButtonText = "Войти",
            DefaultButton = ContentDialogButton.Primary,
            IsPrimaryButtonEnabled = false,
            CloseButtonText = null,
            XamlRoot = XamlRoot,
        };

        var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
        loginPage.Dialog = dialog;
        loginPage.ViewModel.XamlRoot = this.XamlRoot;

        dialog.Content = loginPage;
        dialog.PrimaryButtonClick += loginPage.LoginButtonClick;

        // Открываем ContentDialog
        await dialog.ShowAsync();
    }
}