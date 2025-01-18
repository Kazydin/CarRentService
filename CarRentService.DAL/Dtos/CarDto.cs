﻿using System.Collections.ObjectModel;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class CarDto
{
    [ObservableProperty]
    private int? _id;

    [ObservableProperty]
    private string _registrationNumber;

    /// <summary>
    /// статус автомобиля (например, доступен, в ремонте)
    /// </summary>
    [ObservableProperty]
    private CarStatusEnum _status;

    /// <summary>
    /// Марка транспортного средства.
    /// </summary>
    [ObservableProperty]
    private string _make;

    /// <summary>
    /// Модель транспортного средства.
    /// </summary>
    [ObservableProperty]
    private string _model;

    /// <summary>
    /// Год выпуска транспортного средства.
    /// </summary>
    [ObservableProperty]
    private int? _year;

    [ObservableProperty]
    private string _name;

    #region LinkedEntities

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private RentalDto? _rental;

    [ObservableProperty]
    private BranchDto _branch;

    #endregion

    partial void OnRegistrationNumberChanged(string value)
    {
        UpdateName();
    }

    private new void UpdateName()
    {
        Name = $"{Make} {Model} ({RegistrationNumber})";
    }

    partial void OnMakeChanged(string value)
    {
        UpdateName();
    }

    partial void OnModelChanged(string value)
    {
        UpdateName();
    }
}