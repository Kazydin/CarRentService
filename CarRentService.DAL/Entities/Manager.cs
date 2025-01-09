using System.Collections.ObjectModel;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

public partial class Manager : Person
{
    /// <summary>
    /// Роль
    /// </summary>
    [ObservableProperty]
    private ManagerRoleEnum _role;

    [ObservableProperty]
    private ObservableCollection<Branch> _branches = new();
}