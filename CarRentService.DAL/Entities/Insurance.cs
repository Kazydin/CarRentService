using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Страховка
/// </summary>
[ObservableObject]
public partial class Insurance : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Идентификатор аренды, к которой привязана страховка.
    /// </summary>
    [ObservableProperty]
    private int _rentalId;

    [ObservableProperty]
    private int _carId;

    /// <summary>
    /// Тип страховки (например, полное или частичное покрытие).
    /// </summary>
    [ObservableProperty]
    private InsuranceTypeEnum _type;

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    [ObservableProperty]
    private double _cost;
}