using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Человек
/// </summary>
public abstract class Person : IPersistable
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО человека.
    /// </summary>
    private string _fio;

    /// <summary>
    /// Возраст человека.
    /// </summary>
    private int _age;

    /// <summary>
    /// Контактный телефон человека.
    /// </summary>
    private string _phone;

    private string _login;
    
    private string _password;

    /// <summary>
    /// Возвращает общую информацию о человеке.
    /// </summary>
    /// <returns>Строка с информацией о человеке.</returns>
    public string GetPersonInfo()
    {
        return $"ФИО: {_fio}, Возраст: {_age}, Телефон: {_phone}";
    }

    /// <summary>
    /// Возвращает ФИО человека.
    /// </summary>
    /// <returns>ФИО человека.</returns>
    public string GetFIO()
    {
        return _fio;
    }

    /// <summary>
    /// Устанавливает ФИО человека.
    /// </summary>
    /// <param name="fio">ФИО человека.</param>
    public void SetFIO(string fio)
    {
        _fio = fio;
    }

    /// <summary>
    /// Возвращает возраст человека.
    /// </summary>
    /// <returns>Возраст человека.</returns>
    public int GetAge()
    {
        return _age;
    }

    /// <summary>
    /// Устанавливает возраст человека.
    /// </summary>
    /// <param name="age">Возраст человека.</param>
    public void SetAge(int age)
    {
        _age = age;
    }

    /// <summary>
    /// Возвращает номер телефона человека.
    /// </summary>
    /// <returns>Номер телефона человека.</returns>
    public string GetPhone()
    {
        return _phone;
    }

    /// <summary>
    /// Устанавливает номер телефона человека.
    /// </summary>
    /// <param name="phone">Номер телефона человека.</param>
    public void SetPhone(string phone)
    {
        _phone = phone;
    }
    
    public string GetLogin()
    {
        return _login;
    }
    
    public string GetPassword()
    {
        return _password;
    }

    public void SetLogin(string login)
    {
        _login = login;
    }
    
    public void SetPassword(string password)
    {
        _password = password;
    }
}