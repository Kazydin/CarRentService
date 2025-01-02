using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
public abstract class Vehicle : IPersistable
{
    public int Id { get; set; }

    /// <summary>
    /// Марка транспортного средства.
    /// </summary>
    private string _make;

    /// <summary>
    /// Модель транспортного средства.
    /// </summary>
    private string _model;

    /// <summary>
    /// Год выпуска транспортного средства.
    /// </summary>
    private int _year;

    /// <summary>
    /// Возвращает марку транспортного средства.
    /// </summary>
    /// <returns>Марка транспортного средства.</returns>
    public string GetMake()
    {
        return _make;
    }

    /// <summary>
    /// Устанавливает марку транспортного средства.
    /// </summary>
    /// <param name="make">Марка транспортного средства.</param>
    public void SetMake(string make)
    {
        _make = make;
    }

    /// <summary>
    /// Возвращает модель транспортного средства.
    /// </summary>
    /// <returns>Модель транспортного средства.</returns>
    public string GetModel()
    {
        return _model;
    }

    /// <summary>
    /// Устанавливает модель транспортного средства.
    /// </summary>
    /// <param name="model">Модель транспортного средства.</param>
    public void SetModel(string model)
    {
        _model = model;
    }

    /// <summary>
    /// Возвращает год выпуска транспортного средства.
    /// </summary>
    /// <returns>Год выпуска транспортного средства.</returns>
    public int GetYear()
    {
        return _year;
    }

    /// <summary>
    /// Устанавливает год выпуска транспортного средства.
    /// </summary>
    /// <param name="year">Год выпуска транспортного средства.</param>
    public void SetYear(int year)
    {
        _year = year;
    }
}