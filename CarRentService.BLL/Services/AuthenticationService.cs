using CarRentService.BLL.Services.Abstract;
using CarRentService.DAL;
using CarRentService.DAL.Abstract;

namespace CarRentService.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataStoreContext _store;

        private readonly AppState _state;

        public AuthenticationService(IDataStoreContext store, AppState state)
        {
            this._store = store;
            _state = state;
        }

        public bool Authenticate(string login, string password)
        {
            var user = _store.Manager
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