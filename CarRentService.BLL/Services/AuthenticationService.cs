using CarRentService.BLL.Services.Abstract;
using CarRentService.DAL;
using CarRentService.DAL.Store;

namespace CarRentService.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _store;

        private readonly AppState _state;

        public AuthenticationService(AppState state, AppDbContext store)
        {
            _state = state;
            _store = store;
        }

        public bool Authenticate(string login, string password)
        {
            var user = _store.Managers
                .FirstOrDefault(p => p.Login == login
                                     && p.Password == password);

            if (user != null)
            {
                _state.CurrentUser = user;

                return true;
            }

            return false;
        }
    }
}