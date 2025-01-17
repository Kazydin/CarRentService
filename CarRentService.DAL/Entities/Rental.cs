using System.Collections.ObjectModel;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Constants;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Аренда
/// </summary>
[ObservableObject]
public partial class Rental : IEntity
{
    [ObservableProperty] private int _id;

    /// <summary>
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    [ObservableProperty] private ObservableCollection<int> _carIds = new();

    [ObservableProperty] private ObservableCollection<Car> _cars = new();

    /// <summary>
    /// Идентификатор клиента, который арендует автомобиль.
    /// </summary>
    [ObservableProperty] private int _clientId;

    [ObservableProperty] private Client _client;

    /// <summary>
    /// Идентификатор филиала, где происходит аренда.
    /// </summary>
    [ObservableProperty] private int _branchId;

    [ObservableProperty] private Branch? _branch;

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    [ObservableProperty] private DateTime _startDate;

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    [ObservableProperty] private DateTime _endDate;

    /// <summary>
    /// Статус аренды.
    /// </summary>
    [ObservableProperty] private RentalStatusEnum _status;

    /// <summary>
    /// Цена
    /// </summary>
    [ObservableProperty] private double _cost;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// Считается = Цена - сумма платежей
    /// </summary>
    [ObservableProperty] private double _totalCost;

    [ObservableProperty] private RentalTariffEnum _tariff;

    [ObservableProperty] private string _name;

    [ObservableProperty] private ObservableCollection<Payment> _payments = new();

    [ObservableProperty] private ObservableCollection<Insurance> _insurances = new();

    partial void OnCarsChanged(ObservableCollection<Car> value)
    {
        UpdateName();
    }

    partial void OnClientChanged(Client value)
    {
        ClientId = value.Id;
        UpdateName();
    }

    partial void OnStartDateChanged(DateTime value)
    {
        UpdateName();
    }

    private void UpdateName()
    {
        Name = $"{Id}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Rental other &&
               // Сравниваем по уникальному идентификатору и ключевым полям
               Id == other.Id &&
               StartDate == other.StartDate &&
               EndDate == other.EndDate &&
               Math.Abs(TotalCost - other.TotalCost) < MainConstants.DOUBLE_TOLERANCE &&
               Tariff == other.Tariff &&
               BranchId == other.BranchId &&
               Status == other.Status;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Status, StartDate, EndDate, TotalCost, Tariff, BranchId);
    }
}