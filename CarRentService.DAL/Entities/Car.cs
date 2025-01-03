using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public partial class Car : Vehicle
{
    // Приватное поле - уникальный идентификатор автомобиля
    [ObservableProperty]
    private string _carID;

    // Приватное поле - регистрационный номер автомобиля
    [ObservableProperty]
    private string _registrationNumber;

    // Приватное поле - статус автомобиля (например, доступен, в ремонте)
    [ObservableProperty]
    private string _status;

    // Приватное поле - идентификатор филиала
    [ObservableProperty]
    private string _branchID;
}