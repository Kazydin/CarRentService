using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Человек
/// </summary>
public abstract class Person : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО человека.
    /// </summary>
    public string Fio { get; set; }

    /// <summary>
    /// Возраст человека.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Контактный телефон человека.
    /// </summary>
    public string Phone { get; set; }
}