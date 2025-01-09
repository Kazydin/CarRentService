using System.Collections.ObjectModel;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract;

public interface IDataStoreContext : ICommonDataStoreContext
{
    public ObservableCollection<Client> Client { get; set; }

    public ObservableCollection<Manager> Manager { get; set; }

    public ObservableCollection<Branch> Branch { get; set; }

    public ObservableCollection<Car> Car { get; set; }

    public ObservableCollection<Insurance> Insurance { get; set; }

    public ObservableCollection<Payment> Payment { get; set; }

    public ObservableCollection<rental> Rental { get; set; }
}