using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Abstract.Services;

public interface ICrudService<T> where T : IEntity
{
    ObservableCollection<T> Table { get; }

    T? TryFindById(int id);

    T Add(T entity);

    void Remove(T entity);

    void Update(T entity);
}