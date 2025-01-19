using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Constants;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class RentalDto
{
    [ObservableProperty]
    private int? _id;

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
    private RentalStatusEnum _status;

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
    private RentalTariffEnum _tariff;

    [ObservableProperty]
    private ObservableCollection<int> _carIds = new();

    [ObservableProperty]
    private int? _clientId;

    #region LinkedEntities

    [ObservableProperty]
    private ObservableCollection<CarDto> _cars = new();

    [ObservableProperty]
    private BranchDto? _branch;

    [ObservableProperty]
    private ClientDto? _client;

    [ObservableProperty]
    private ObservableCollection<PaymentDto> _payments = new();

    [ObservableProperty]
    private ObservableCollection<InsuranceDto> _insurances = new();

    #endregion

    public override bool Equals(object? obj)
    {
        return obj is RentalDto other &&
               // Сравниваем по уникальному идентификатору и ключевым полям
               Id == other.Id &&
               StartDate == other.StartDate &&
               EndDate == other.EndDate &&
               Math.Abs(TotalCost - other.TotalCost) < MainConstants.DOUBLE_TOLERANCE &&
               Tariff == other.Tariff &&
               Status == other.Status;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Status, StartDate, EndDate, TotalCost, Tariff);
    }
}