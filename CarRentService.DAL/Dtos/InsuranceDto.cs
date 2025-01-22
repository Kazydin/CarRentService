using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class InsuranceDto
{
    [ObservableProperty] private int? _id;

    /// <summary>
    /// Тип страховки (например, полное или частичное покрытие).
    /// </summary>
    [ObservableProperty] private InsuranceTypeEnum _type;

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    [ObservableProperty] private double _cost;

    #region LinkedEntities

    [ObservableProperty] private RentalDto _rental;

    [ObservableProperty] private CarDto _car;

    #endregion
}