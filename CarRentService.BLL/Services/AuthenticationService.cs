using CarRentService.BLL.Services.Abstract;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;

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
            var d = _store.GetTable<Client>();

            var user = _store
                .GetTable<Client>()
                .FirstOrDefault(p => p.GetLogin() == login
                                     && p.GetPassword() == password);

            if (user != null)
            {
                _state.CurrentUser = user;

                return true;
            }

            return false;
        }
    }
}