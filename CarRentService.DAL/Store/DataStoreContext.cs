using CarRentService.DAL.Abstract;
using CarRentService.DAL.Contracs;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Store
{
    public class DataStoreContext : IDataStoreContext
    {
        // Коллекции, имитирующие таблицы данных
        public List<Client> Clients { get; set; } = [];

        public List<Admin> Admins { get; set; } = [];

        public List<Branch> Branches { get; set; } = [];

        public List<Car> Cars { get; set; } = [];

        public List<Insurance> Insurances { get; set; } = [];

        public List<Payment> Payments { get; set; } = [];

        public List<Rental> Rentals { get; set; } = [];

        public List<T> GetTable<T>() where T : IEntity
        {
            // Используем рефлексию, чтобы найти нужную коллекцию по типу
            var property = GetType().GetProperty(typeof(T).Name);
            if (property == null)
            {
                throw new ArgumentException($"No table found for type {typeof(T).Name}");
            }

            return (property.GetValue(this) as List<T>)!;
        }

        public void Add<T>(T entity) where T : IEntity
        {
            var table = GetTable<T>();
            table.Add(entity);
        }

        public void Remove<T>(T entity) where T : IEntity
        {
            var table = GetTable<T>();
            table.Remove(entity);
        }

        public void Remove<T>(Predicate<T> predicate) where T : IEntity
        {
            var table = GetTable<T>();
            var itemToRemove = table.Find(predicate);
            if (itemToRemove != null)
            {
                table.Remove(itemToRemove);
            }
        }
    }
}