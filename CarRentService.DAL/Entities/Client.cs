using CarRentService.DAL.Contracs;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    private string _driverLicenseNumber;

    /// <summary>
    /// История аренд клиента
    /// </summary>
    private List<Rental> _rentalHistory = new List<Rental>();

    /// <summary>
    /// Возвращает информацию о клиенте, включая ФИО, возраст, телефон и номер водительского удостоверения.
    /// </summary>
    /// <returns>Строка с информацией о клиенте.</returns>
    public string GetClientInfo()
    {
        return
            $"ФИО: {GetFIO()}, Возраст: {GetAge()}, Телефон: {GetPhone()}, Водительское удостоверение: {_driverLicenseNumber}";
    }

    /// <summary>
    /// Добавляет аренду в историю аренд клиента.
    /// </summary>
    /// <param name="rental">Объект аренды для добавления в историю.</param>
    public void AddRental(Rental rental)
    {
        _rentalHistory.Add(rental);
    }

    /// <summary>
    /// Возвращает список всех аренд клиента.
    /// </summary>
    /// <returns>Список объектов аренды.</returns>
    public List<Rental> GetRentalHistory()
    {
        return new List<Rental>(_rentalHistory); // Возвращаем копию списка
    }

    /// <summary>
    /// Обновляет номер водительского удостоверения.
    /// </summary>
    /// <param name="newLicenseNumber">Новый номер водительского удостоверения.</param>
    public void UpdateDriverLicense(string newLicenseNumber)
    {
        _driverLicenseNumber = newLicenseNumber;
    }

    /// <summary>
    /// Возвращает номер водительского удостоверения клиента.
    /// </summary>
    /// <returns>Номер водительского удостоверения.</returns>
    public string GetDriverLicenseNumber()
    {
        return _driverLicenseNumber;
    }

    /// <summary>
    /// Устанавливает номер водительского удостоверения.
    /// </summary>
    /// <param name="licenseNumber">Номер водительского удостоверения.</param>
    public void SetDriverLicenseNumber(string licenseNumber)
    {
        _driverLicenseNumber = licenseNumber;
    }
}