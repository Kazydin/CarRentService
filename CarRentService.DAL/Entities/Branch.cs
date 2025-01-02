using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Филиал
/// </summary>
public class Branch : IPersistable
{
    public int Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор филиала.
    /// </summary>
    private string _branchID;

    /// <summary>
    /// Название филиала.
    /// </summary>
    private string _name;

    /// <summary>
    /// Адрес филиала.
    /// </summary>
    private string _address;

    /// <summary>
    /// Количество автомобилей в филиале.
    /// </summary>
    private int _numberOfCars;

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    private string _contactDetails;

    /// <summary>
    /// Список автомобилей, принадлежащих филиалу.
    /// </summary>
    private List<Car> _carList = new List<Car>();

    /// <summary>
    /// Возвращает информацию о филиале.
    /// </summary>
    /// <returns>Строка с информацией о филиале.</returns>
    public string GetBranchInfo()
    {
        return $"BranchID: {_branchID}, Название: {_name}, Адрес: {_address}, " +
               $"Количество автомобилей: {_numberOfCars}, Контакты: {_contactDetails}";
    }

    /// <summary>
    /// Возвращает список доступных автомобилей в филиале.
    /// </summary>
    /// <returns>Список доступных автомобилей.</returns>
    public List<Car> GetAvailableCars()
    {
        return _carList.FindAll(car => car.GetStatus() == "Доступен");
    }

    /// <summary>
    /// Добавляет автомобиль в филиал.
    /// </summary>
    /// <param name="car">Объект автомобиля для добавления.</param>
    public void AddCar(Car car)
    {
        _carList.Add(car);
        _numberOfCars = _carList.Count;
    }

    /// <summary>
    /// Удаляет автомобиль из филиала.
    /// </summary>
    /// <param name="car">Объект автомобиля для удаления.</param>
    public void RemoveCar(Car car)
    {
        _carList.Remove(car);
        _numberOfCars = _carList.Count;
    }

    /// <summary>
    /// Обновляет контактные данные филиала.
    /// </summary>
    /// <param name="newContactDetails">Новые контактные данные филиала.</param>
    public void UpdateContactDetails(string newContactDetails)
    {
        _contactDetails = newContactDetails;
    }

    /// <summary>
    /// Обновляет количество автомобилей в филиале.
    /// </summary>
    /// <param name="newCount">Новое количество автомобилей.</param>
    public void UpdateNumberOfCars(int newCount)
    {
        _numberOfCars = newCount;
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