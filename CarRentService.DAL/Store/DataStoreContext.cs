using System.Collections.ObjectModel;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Store;

public sealed partial class DataStoreContext : ObservableObject, IDataStoreContext
{
    [ObservableProperty]
    private ObservableCollection<Client> _client = [];

    [ObservableProperty]
    private ObservableCollection<Manager> _manager = [];

    [ObservableProperty]
    private ObservableCollection<Branch> _branch = [];

    [ObservableProperty]
    private ObservableCollection<Car> _car = [];

    [ObservableProperty]
    private ObservableCollection<Insurance> _insurance = [];

    [ObservableProperty]
    private ObservableCollection<Payment> _payment = [];

    [ObservableProperty]
    private ObservableCollection<Rental> _rental = [];

    public T Add<T>(T entity) where T : IEntity
    {
        var table = GetTable<T>();

        if (entity.Id == 0)
        {
            entity.Id = table.Any() ? table.Max(p => p.Id) + 1 : 1;
        }

        table.Add(entity);

        return entity;
    }

    public void Remove<T>(T entity) where T : IEntity
    {
        var table = GetTable<T>();
        table.Remove(entity);
    }

    public void Remove<T>(Predicate<T> predicate) where T : IEntity
    {
        var table = GetTable<T>();
        var itemsToRemove = table.Where(item => predicate(item));
        foreach (var entity in itemsToRemove)
        {
            table.Remove(entity);
        }
    }

    private ObservableCollection<T> GetTable<T>() where T : IEntity
    {
        // Используем рефлексию, чтобы найти нужную коллекцию по типу
        var property = GetType().GetProperty(typeof(T).Name);
        if (property == null)
        {
            throw new ArgumentException($"No table found for type {typeof(T).Name}");
        }

        return (property.GetValue(this) as ObservableCollection<T>)!;
    }
}