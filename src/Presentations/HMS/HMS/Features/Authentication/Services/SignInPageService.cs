using CommunityToolkit.Diagnostics;
using HMS.Domain;

namespace HMS;

public class SignInPageService : ISignInPageService
{
    #region [ Fields ]

    private readonly IAuthenticationService authenticationService;
    private readonly ILocalStorageService localStorageService;
    private readonly IInternetService internetService;
    private readonly IUserService userService;

    #endregion

    #region [ CTors ]

    public SignInPageService(IAuthenticationService authenticationService,
                         ILocalStorageService localStorageService,
                         IInternetService internetService,
                         IUserService userService)
    {
        this.authenticationService = authenticationService;
        this.localStorageService = localStorageService;
        this.internetService = internetService;
        this.userService = userService;
    }
    #endregion

    #region [ Methods ]


    public async Task<User> GetCurrentUserAsync()
    {
        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        if (accessToken is null)
            return null;

        return await userService.GetCurrentUserAsync(accessToken);
    }

    public bool IsInternetAvailable()
        => internetService.IsInternetAvailable();

    public async Task<UserLoginInfo?> LoginAsync(string email, string password)
    {
        var authenticationResult = await authenticationService.LoginAsync(email, password);
        if (authenticationResult.IsSuccess)
            return new(authenticationResult.Token, authenticationResult.UserInfo.ID, authenticationResult.RequestAt, authenticationResult.ExpiredIn);
        else
            return null;
    }

    public async Task SaveUserAccessTokenAsync(string accessToken)
    {
        Guard.IsNotNullOrEmpty(accessToken);
        await localStorageService.WriteValueAsync("accesstoken", accessToken);
    }

    public Task SaveUserGuidAsync(string userguid)
    {
        localStorageService.WriteValueAsync("userguid", userguid);
        return Task.CompletedTask;
    }
    #endregion
}
