using System;
using CarRentService.Common.Abstract;
using CarRentService.DAL;
using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Pages.Welcome;

public partial class WelcomeViewModel : IViewModel, INotifiable
{
    [ObservableProperty]
    private AppState _appState;

    [ObservableProperty]
    private string _userName;

    public WelcomeViewModel(AppState appState)
    {
        AppState = appState;
        AppState.Subscribe(this);
    }

    public void Update(object sender, EventArgs e)
    {
        UserName = AppState.CurrentUser?.Fio;
    }
}