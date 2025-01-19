namespace CarRentService.DAL.Abstract;

public interface INotifiable
{
    void Update(object sender, EventArgs e);
}