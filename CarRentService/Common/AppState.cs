using System;
using Windows.System;

namespace CarRentService.Common;

public class AppState
{
    private User? _currentUser;

    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnUserChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? OnUserChanged;
}