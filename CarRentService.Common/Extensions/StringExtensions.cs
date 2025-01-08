namespace CarRentService.Common.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Пытается преобразовать строку в int. Возвращает значение по умолчанию, если преобразование не удалось.
    /// </summary>
    /// <param name="input">Строка для преобразования.</param>
    /// <param name="defaultValue">Значение по умолчанию, возвращаемое при ошибке.</param>
    /// <returns>Преобразованное значение или значение по умолчанию.</returns>
    public static int TryInt(this string input, int defaultValue = 0)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }

        return defaultValue;
    }
}