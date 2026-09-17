namespace MyBlazorApp.Services;

/// <summary>
/// Minimal client-side "session" - just enough to gate the dashboard
/// behind the login screen and greet the user by name. There's no
/// real backend here; replace this with real authentication before
/// this app touches anything that matters.
/// </summary>
public class AuthState
{
    public string? CurrentUser { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(CurrentUser);

    public event Action? Changed;

    public void LogIn(string username)
    {
        CurrentUser = username;
        Changed?.Invoke();
    }

    public void LogOut()
    {
        CurrentUser = null;
        Changed?.Invoke();
    }
}
