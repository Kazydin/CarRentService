using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Страховка
/// </summary>
public class Insurance : IPersistable
{
    public int Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор страховки.
    /// </summary>
    private string _insuranceID;

    /// <summary>
    /// Идентификатор клиента, которому принадлежит страховка.
    /// </summary>
    private string _clientID;

    /// <summary>
    /// Идентификатор аренды, к которой привязана страховка.
    /// </summary>
    private string _rentalID;

    /// <summary>
    /// Сумма покрытия страховки.
    /// </summary>
    private double _coverageAmount;

    /// <summary>
    /// Дата начала страховки.
    /// </summary>
    private DateTime _startDate;

    /// <summary>
    /// Дата окончания страховки.
    /// </summary>
    private DateTime _endDate;

    /// <summary>
    /// Тип страховки (например, полное или частичное покрытие).
    /// </summary>
    private string _type;

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    private double _cost;

    /// <summary>
    /// Автомобиль, на который оформлена страховка.
    /// </summary>
    private Car _car;

    /// <summary>
    /// Возвращает информацию о страховке.
    /// </summary>
    /// <returns>Строка с информацией о страховке.</returns>
    public string GetInsuranceInfo()
    {
        return $"InsuranceID: {_insuranceID}, ClientID: {_clientID}, RentalID: {_rentalID}, " +
               $"CoverageAmount: {_coverageAmount}, StartDate: {_startDate}, EndDate: {_endDate}, " +
               $"Type: {_type}, Cost: {_cost}, CarID: {_car?.GetCarID()}";
    }

    /// <summary>
    /// Обновляет сумму покрытия страховки.
    /// </summary>
    /// <param name="newCoverageAmount">Новая сумма покрытия страховки.</param>
    public void UpdateCoverageAmount(double newCoverageAmount)
    {
        _coverageAmount = newCoverageAmount;
    }

    /// <summary>
    /// Продлевает страховку на указанное количество дней.
    /// </summary>
    /// <param name="duration">Количество дней для продления страховки.</param>
    public void ExtendInsurance(int duration)
    {
        _endDate = _endDate.AddDays(duration);
    }

    /// <summary>
    /// Рассчитывает стоимость страховки.
    /// </summary>
    /// <returns>Стоимость страховки.</returns>
    public double CalculateInsuranceCost()
    {
        double days = (_endDate - _startDate).TotalDays;
        return days * 10; // Пример расчета: 10 единиц за день
    }

    /// <summary>
    /// Возвращает идентификатор страховки.
    /// </summary>
    /// <returns>Идентификатор страховки.</returns>
    public string GetInsuranceID()
    {
        return _insuranceID;
    }

    /// <summary>
    /// Устанавливает идентификатор страховки.
    /// </summary>
    /// <param name="id">Идентификатор страховки.</param>
    public void SetInsuranceID(string id)
    {
        _insuranceID = id;
    }
}