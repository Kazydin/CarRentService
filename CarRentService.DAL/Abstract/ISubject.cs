namespace CarRentService.DAL.Abstract;

public interface ISubject
{
    void Subscribe(INotifiable observer);
    void Unsubscribe(INotifiable observer);
    void Notify();
}