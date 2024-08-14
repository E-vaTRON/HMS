
using CommunityToolkit.Diagnostics;

namespace HMS;

public class ChangePasswordPageService : IChangePasswordPageService
{
    #region [ Fields ]

    private readonly IUserService userService;
    private readonly ILocalStorageService localStorageService;
    private readonly IAuthenticationService authenticationService;
    #endregion

    #region [ CTors ]

    public ChangePasswordPageService(IUserService userService,
                                     ILocalStorageService localStorageService,
                                     IAuthenticationService authenticationService)
    {
        this.userService = userService;
        this.localStorageService = localStorageService;
        this.authenticationService = authenticationService;
    }
    #endregion

    #region [ Methods ]

    public async Task<ChangePasswordResult> ChangePassword(string currentPassword, string newPassword)
    {
        Guard.IsNotNullOrEmpty(currentPassword, nameof(currentPassword));
        Guard.IsNotNullOrEmpty(newPassword, nameof(newPassword));

        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        if (accessToken is null)
            return new()
            {
                IsSuccess = false,
                Detail = "Can't find access token in secure storage"
            };

        var currentUser = await userService.GetCurrentUserAsync(accessToken);
        if (currentUser is null)
            return new()
            {
                IsSuccess = false,
                Detail = "Can't find this user on the server"
            };

        var result = await authenticationService.ChangePassword(currentUser.ID, currentPassword, newPassword);
        return new() { IsSuccess = result.IsSuccess, Detail = result.Content };

    }

    public Task RemoveUserInfo()
        => Task.FromResult(() => localStorageService.RemoveAllValue());
    #endregion
}
