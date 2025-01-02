using CarRentService.DAL.Contracs;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Store;

public class StoreContext
{
    public List<Client> Clients = new();
    
    public List<Admin> Admins = new();
    
    public List<Branch> Branches = new();
    
    public List<Car> Cars = new();
    
    public List<Insurance> Insurances = new();
    
    public List<Payment> Payments = new();
    
    public List<Rental> Rentals = new();
}