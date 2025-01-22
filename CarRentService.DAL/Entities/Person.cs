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

    // protected bool Equals(Person other)
    // {
    //     return Id == other.Id && Fio == other.Fio && Age == other.Age && Phone == other.Phone;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (obj is null) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((Person)obj);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(Id, Fio, Age, Phone);
    // }
}