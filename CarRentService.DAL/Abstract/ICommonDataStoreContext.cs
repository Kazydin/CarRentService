﻿namespace CarRentService.DAL.Abstract;

public interface ICommonDataStoreContext
{
    /// <summary>
    /// Добавить запись в таблицу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    T Add<T>(T entity) where T : IEntity;

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