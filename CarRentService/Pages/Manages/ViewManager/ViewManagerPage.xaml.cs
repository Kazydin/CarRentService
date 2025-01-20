using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using Microsoft.UI.Xaml;
using Syncfusion.UI.Xaml.Editors;

namespace CarRentService.Pages.Manages.ViewManager;

public sealed partial class ViewManagerPage : NavigationPage
{
    private readonly ViewManagerViewModel _viewModel;

    public ViewManagerPage(ViewManagerViewModel viewModel) : base(PageTypeEnum.EditManager)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId, ManagerBranches.SelectedItems);

            PasswordBox.Password = _viewModel.Manager.Password;

            Header = $"Редактирование менеджера № {_viewModel.Manager.Id!.Value}";
        }
        else
        {
            await _viewModel.UpdateState();
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
                _viewModel.SelectedBranches.Remove((BranchDto)removedItem);
            }

            // Добавляем новые элементы
            foreach (var addedItem in e.AddedItems)
            {
                _viewModel.SelectedBranches.Add((BranchDto)addedItem);
            }
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Manager.Password = PasswordBox.Password;
    }
}