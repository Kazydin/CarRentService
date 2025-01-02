using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void MainWindow_OnActivated(object sender, WindowActivatedEventArgs args)
        {
            // Ожидаем, пока окно отобразится
            var dialog = new ContentDialog
            {
                Title = "Добро пожаловать!",
                Content = "Здесь можно разместить любой текст.",
                CloseButtonText = "Закрыть",
                XamlRoot = this.Content.XamlRoot
            };

            // Открываем ContentDialog
            await dialog.ShowAsync();
        }
    }
}
