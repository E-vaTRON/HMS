using HMS.Domain;

namespace HMS;

public interface ISignInPageService
{
    bool IsInternetAvailable();
    Task<UserLoginInfo?> LoginAsync(string email, string password);
    Task SaveUserAccessTokenAsync(string accessToken);
    Task SaveUserGuidAsync(string userguid);
    Task<User?> GetCurrentUserAsync();
}
