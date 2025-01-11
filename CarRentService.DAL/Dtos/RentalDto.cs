using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class RentalDto
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private ObservableCollection<CarDto> _cars;

    [ObservableProperty]
    private Branch? _branch;

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
    /// Цена
    /// </summary>
    [ObservableProperty]
    private double _cost;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// Считается = Цена - сумма платежей
    /// </summary>
    [ObservableProperty]
    private double _totalCost;
}