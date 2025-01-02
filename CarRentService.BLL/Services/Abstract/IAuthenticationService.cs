using CarRentService.Common.Attributes;

namespace CarRentService.BLL.Services.Abstract;

[InjectDI]
public interface IAuthenticationService
{
    bool Authenticate(string login, string password);
}