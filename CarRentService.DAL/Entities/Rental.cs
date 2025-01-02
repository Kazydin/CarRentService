using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Аренда
/// </summary>
public class Rental : IPersistable
{
    public int Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор аренды.
    /// </summary>
    private string _rentalID;

    /// <summary>
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    private string _carID;

    /// <summary>
    /// Идентификатор клиента, который арендует автомобиль.
    /// </summary>
    private string _clientID;

    /// <summary>
    /// Идентификатор филиала, где происходит аренда.
    /// </summary>
    private string _branchID;

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    private DateTime _startDate;

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    private DateTime _endDate;

    /// <summary>
    /// Статус аренды.
    /// </summary>
    private string _status;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// </summary>
    private double _totalCost;

    /// <summary>
    /// Возвращает информацию об аренде.
    /// </summary>
    /// <returns>Строка с информацией об аренде.</returns>
    public string GetRentalInfo()
    {
        return $"RentalID: {_rentalID}, CarID: {_carID}, ClientID: {_clientID}, BranchID: {_branchID}, " +
               $"StartDate: {_startDate}, EndDate: {_endDate}, Status: {_status}, TotalCost: {_totalCost}";
    }

    /// <summary>
    /// Продлевает аренду на указанное количество дней.
    /// </summary>
    /// <param name="duration">Количество дней для продления аренды.</param>
    public void ExtendRental(int duration)
    {
        _endDate = _endDate.AddDays(duration);
    }

    /// <summary>
    /// Обновляет статус аренды.
    /// </summary>
    /// <param name="newStatus">Новый статус аренды.</param>
    public void UpdateRentalStatus(string newStatus)
    {
        _status = newStatus;
    }

    /// <summary>
    /// Рассчитывает итоговую стоимость аренды на основе срока.
    /// </summary>
    /// <returns>Итоговая стоимость аренды.</returns>
    public double CalculateTotalCost()
    {
        int rentalDays = (int)(_endDate - _startDate).TotalDays;
        return rentalDays * 50.0; // Пример расчета: 50 единиц за день
    }

    /// <summary>
    /// Возвращает идентификатор аренды.
    /// </summary>
    /// <returns>Идентификатор аренды.</returns>
    public string GetRentalID()
    {
        return _rentalID;
    }

    /// <summary>
    /// Устанавливает идентификатор аренды.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    public void SetRentalID(string id)
    {
        _rentalID = id;
    }

    /// <summary>
    /// Возвращает идентификатор автомобиля.
    /// </summary>
    /// <returns>Идентификатор автомобиля.</returns>
    public string GetCarID()
    {
        return _carID;
    }

    /// <summary>
    /// Устанавливает идентификатор автомобиля.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    public void SetCarID(string id)
    {
        _carID = id;
    }

    /// <summary>
    /// Возвращает идентификатор клиента.
    /// </summary>
    /// <returns>Идентификатор клиента.</returns>
    public string GetClientID()
    {
        return _clientID;
    }

    /// <summary>
    /// Устанавливает идентификатор клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    public void SetClientID(string id)
    {
        _clientID = id;
    }

    /// <summary>
    /// Возвращает идентификатор филиала.
    /// </summary>
    /// <returns>Идентификатор филиала.</returns>
    public string GetBranchID()
    {
        return _branchID;
    }

    /// <summary>
    /// Устанавливает идентификатор филиала.
    /// </summary>
    /// <param name="id">Идентификатор филиала.</param>
    public void SetBranchID(string id)
    {
        _branchID = id;
    }
}