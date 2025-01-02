using CarRentService.DAL.Contracs;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public class Car : Vehicle
{
    // Приватное поле - уникальный идентификатор автомобиля
    private string _carID;

    // Приватное поле - регистрационный номер автомобиля
    private string _registrationNumber;

    // Приватное поле - статус автомобиля (например, доступен, в ремонте)
    private string _status;

    // Приватное поле - идентификатор филиала
    private string _branchID;

    /// <summary>
    /// Возвращает информацию об автомобиле.
    /// </summary>
    /// <returns>Строка с информацией об автомобиле.</returns>
    public string GetCarInfo()
    {
        return $"ID: {_carID}, Регистрационный номер: {_registrationNumber}, Статус: {_status}, Филиал: {_branchID}, Марка: {GetMake()}, Модель: {GetModel()}, Год: {GetYear()}";
    }

    /// <summary>
    /// Обновляет статус автомобиля.
    /// </summary>
    /// <param name="newStatus">Новый статус автомобиля.</param>
    public void UpdateCarStatus(string newStatus)
    {
        _status = newStatus;
    }

    /// <summary>
    /// Перемещает автомобиль в другой филиал.
    /// </summary>
    /// <param name="newBranchID">Новый идентификатор филиала.</param>
    public void MoveToBranch(string newBranchID)
    {
        _branchID = newBranchID;
    }

    /// <summary>
    /// Обновляет регистрационный номер и статус автомобиля.
    /// </summary>
    /// <param name="registrationNumber">Новый регистрационный номер автомобиля.</param>
    /// <param name="status">Новый статус автомобиля.</param>
    public void UpdateCarDetails(string registrationNumber, string status)
    {
        _registrationNumber = registrationNumber;
        _status = status;
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
    /// Возвращает регистрационный номер автомобиля.
    /// </summary>
    /// <returns>Регистрационный номер автомобиля.</returns>
    public string GetRegistrationNumber()
    {
        return _registrationNumber;
    }

    /// <summary>
    /// Устанавливает регистрационный номер автомобиля.
    /// </summary>
    /// <param name="number">Регистрационный номер автомобиля.</param>
    public void SetRegistrationNumber(string number)
    {
        _registrationNumber = number;
    }

    /// <summary>
    /// Возвращает статус автомобиля.
    /// </summary>
    /// <returns>Статус автомобиля.</returns>
    public string GetStatus()
    {
        return _status;
    }

    /// <summary>
    /// Устанавливает статус автомобиля.
    /// </summary>
    /// <param name="status">Статус автомобиля.</param>
    public void SetStatus(string status)
    {
        _status = status;
    }
}