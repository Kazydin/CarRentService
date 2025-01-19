using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL;

[InjectDI(ServiceLifetime.Singleton)]
public class AppState : ISubject
{
    private readonly List<INotifiable> _observers = new();

    private Manager? _currentUser;

    public Manager? CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                Notify();
            }
        }
    }

    public void Subscribe(INotifiable observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(INotifiable observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this, EventArgs.Empty);
        }
    }
}