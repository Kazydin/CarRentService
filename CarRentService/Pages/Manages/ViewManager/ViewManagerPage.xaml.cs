using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;
using CarRentService.DAL.Entities;
using Microsoft.UI.Xaml;
using Syncfusion.UI.Xaml.Editors;

namespace CarRentService.Pages.Manages.ViewManager;

public sealed partial class ViewManagerPage : NavigationPage
{
    private readonly ViewManagerViewModel _viewModel;

    public ViewManagerPage(ViewManagerViewModel viewModel) : base(PageTypeEnum.EditManager, "Редактирование менеджера")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetManager(data.EntityId, ManagerBranches.SelectedItems);

            PasswordBox.Password =  _viewModel.Manager.Password;

            Header = data.Header;
        }
        else
        {
            _viewModel.SetManager();
            Header = "Создание менеджера";
        }
    }

    private void ManagerBranches_OnSelectionChanged(object? sender, ComboBoxSelectionChangedEventArgs e)
    {
        if (sender is SfComboBox)
        {
            // Удаляем элементы, которые были убраны
            foreach (var removedItem in e.RemovedItems)
            {
                _viewModel.SelectedBranches.Remove((Branch)removedItem);
            }

            // Добавляем новые элементы
            foreach (var addedItem in e.AddedItems)
            {
                _viewModel.SelectedBranches.Add((Branch)addedItem);
            }
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Manager.Password = PasswordBox.Password;
    }
}