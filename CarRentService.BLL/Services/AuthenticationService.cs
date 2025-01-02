using CarRentService.BLL.Services.Abstract;

namespace CarRentService.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string login, string password)
        {
            return login == "admin" && password == "admin123456";
        }
    }
}
