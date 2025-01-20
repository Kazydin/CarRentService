using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Филиал
/// </summary>
public class Branch : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Название филиала.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Адрес филиала.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    public string ContactDetails { get; set; }

    public List<Manager> Managers { get; set; }

    public List<Client> Clients { get; set; }

    public List<Car> Cars { get; set; }
}