using System.Collections.ObjectModel;

namespace CarRentService.DAL.Abstract.Services;

public interface ICrudService<T> where T : IEntity
{
    ObservableCollection<T> Table { get; }

    T? TryFindById(int id);

    T Add(T entity);

    void Remove(int entityId);

    void Remove(T entity);

    void Update(T entity);

    T AddOrUpdate(T entity);
}