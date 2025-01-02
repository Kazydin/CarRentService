namespace CarRentService.BLL.Services.Abstract;

public interface IAuthenticationService
{
    bool Authenticate(string login, string password);
}