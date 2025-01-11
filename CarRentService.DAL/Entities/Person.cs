using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Человек
/// </summary>
[ObservableObject]
public abstract partial class Person : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// ФИО человека.
    /// </summary>
    [ObservableProperty]
    private string _fio;

    /// <summary>
    /// Возраст человека.
    /// </summary>
    [ObservableProperty]
    private int _age;

    /// <summary>
    /// Контактный телефон человека.
    /// </summary>
    [ObservableProperty]
    private string _phone;
}