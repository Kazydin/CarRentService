using CarRentService.DAL.Contracs;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Платеж
/// </summary>
public class Payment
{
    /// <summary>
    /// Уникальный идентификатор платежа.
    /// </summary>
    private string _paymentID;

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    private double _amount;

    /// <summary>
    /// Дата платежа.
    /// </summary>
    private DateTime _date;

    /// <summary>
    /// Метод оплаты (например, наличные, карта).
    /// </summary>
    private string _method;

    /// <summary>
    /// Аренда, связанная с этим платежом.
    /// </summary>
    private Rental _rental;

    /// <summary>
    /// Возвращает информацию о платеже.
    /// </summary>
    /// <returns>Строка с информацией о платеже.</returns>
    public string GetPaymentInfo()
    {
        return $"PaymentID: {_paymentID}, Сумма: {_amount}, Дата: {_date}, Метод: {_method}, АрендаID: {_rental?.GetRentalID()}";
    }

    /// <summary>
    /// Рассчитывает общую сумму всех платежей.
    /// </summary>
    /// <param name="payments">Список платежей.</param>
    /// <returns>Общая сумма всех платежей.</returns>
    public double CalculateTotalPayments(List<Payment> payments)
    {
        double total = 0;
        foreach (var payment in payments)
        {
            total += payment.GetAmount();
        }
        return total;
    }

    /// <summary>
    /// Обрабатывает платеж, устанавливая сумму, метод оплаты и дату.
    /// </summary>
    /// <param name="amount">Сумма платежа.</param>
    /// <param name="method">Метод оплаты.</param>
    public void ProcessPayment(double amount, string method)
    {
        _amount = amount;
        _method = method;
        _date = DateTime.Now;
    }

    /// <summary>
    /// Возвращает идентификатор платежа.
    /// </summary>
    /// <returns>Идентификатор платежа.</returns>
    public string GetPaymentID()
    {
        return _paymentID;
    }

    /// <summary>
    /// Устанавливает идентификатор платежа.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    public void SetPaymentID(string id)
    {
        _paymentID = id;
    }

    /// <summary>
    /// Возвращает сумму платежа.
    /// </summary>
    /// <returns>Сумма платежа.</returns>
    public double GetAmount()
    {
        return _amount;
    }

    /// <summary>
    /// Устанавливает сумму платежа.
    /// </summary>
    /// <param name="amount">Сумма платежа.</param>
    public void SetAmount(double amount)
    {
        _amount = amount;
    }
}