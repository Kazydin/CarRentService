using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Abstract;

[InjectDI]
public interface IDataStoreContext
{
    /// <summary>
    /// Получить таблицу данных по типу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    List<T> GetTable<T>() where T : IEntity;

    /// <summary>
    /// Добавить запись в таблицу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Add<T>(T entity) where T : IEntity;

    /// <summary>
    /// Удалить запись из таблицы
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Remove<T>(T entity) where T : IEntity;

    /// <summary>
    /// Удалить запись по условию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="predicate"></param>
    void Remove<T>(Predicate<T> predicate) where T : IEntity;
}