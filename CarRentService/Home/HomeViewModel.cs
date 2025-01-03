﻿using System.Windows.Input;
using CarRentService.Login;
using Microsoft.UI.Xaml.Controls;

using Microsoft.UI.Xaml;
using CarRentService.Home.Pages.Domain;
using System.Collections.Generic;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentService.Home
{
    public partial class HomeViewModel : IViewModel
    {
        public ICommand ShowLoginDialogCommand { get; }

        public ICommand NavigateCommand { get; }

        public XamlRoot XamlRoot = null!;

        public readonly Dictionary<HomePageTypeEnum, InjectedHomePage> _pages;

        [ObservableProperty]
        private string _header;

        [ObservableProperty]
        private InjectedHomePage _currentPage;

        private readonly LoginPage _loginPage;

        public HomeViewModel(LoginPage loginPage, HomePageFactory factory)
        {
            _loginPage = loginPage;
            ShowLoginDialogCommand = new RelayCommand(ShowLoginDialog);
            NavigateCommand = new RelayCommand<HomePageTypeEnum>(Navigate);

            _pages = factory.GetPages();
            CurrentPage = _pages[HomePageTypeEnum.Welcome];
        }

        private async void ShowLoginDialog()
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

            _loginPage.Dialog = dialog;

            dialog.Content = _loginPage;
            dialog.PrimaryButtonClick += _loginPage.LoginButtonClick;

            // TODO: удалить позже
            _loginPage.ViewModel.Login = "admin";
            _loginPage.ViewModel.Password = "admin123";
            _loginPage.ViewModel.Authenticate();

            // Открываем ContentDialog
            // await dialog.ShowAsync();
        }

        private void Navigate(HomePageTypeEnum pageKey)
        {
            if (_pages.TryGetValue(pageKey, out var pageType))
            {
                CurrentPage = pageType;
                Header = pageKey.GetDescription(); // Или другая логика для заголовка
            }
        }
    }
}
