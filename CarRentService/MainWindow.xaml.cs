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
            // �������, ���� ���� �����������
            var dialog = new ContentDialog
            {
                Title = "����� ����������!",
                Content = "����� ����� ���������� ����� �����.",
                CloseButtonText = "�������",
                XamlRoot = this.Content.XamlRoot
            };

            // ��������� ContentDialog
            await dialog.ShowAsync();
        }
    }
}
