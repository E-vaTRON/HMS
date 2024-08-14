namespace HMS.Contract;

public interface IAuthenticationService
{
    Task<AuthenticationResult> LoginAsync(string email, string password);

    Task<ChangePasswordResult> ChangePassword(string userId, string currentPassword, string newPassword);
}
