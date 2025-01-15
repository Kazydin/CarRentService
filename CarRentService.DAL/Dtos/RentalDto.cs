using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class RentalDto
{
    [ObservableProperty]
    private int? _id;

    [ObservableProperty]
    private ObservableCollection<CarDto> _cars = new();

    [ObservableProperty]
    private Branch? _branch;

    [ObservableProperty]
    private ClientDto? _client;

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    [ObservableProperty]
    private DateTime _startDate;

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    [ObservableProperty]
    private DateTime _endDate;

    /// <summary>
    /// Статус аренды.
    /// </summary>
    [ObservableProperty]
    private string _status;

    /// <summary>
    /// Сумма платежей
    /// </summary>
    [ObservableProperty]
    private double _totalPaymentsSum;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// Считается = Цена - сумма платежей
    /// </summary>
    [ObservableProperty]
    private double _totalCost;

    [ObservableProperty]
    private string _tariff;

    [ObservableProperty]
    private ObservableCollection<Payment> _payments = new();

    [ObservableProperty]
    private ObservableCollection<Insurance> _insurances = new();
}