namespace CarRentService.DAL.Abstract;

public class BaseSubject : ISubject
{
    private readonly List<INotifiable> _observers = new();

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