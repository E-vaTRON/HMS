namespace HMS.LogicProvider;

public class AuthenticationService : IAuthenticationService
{
    #region [ Fields ]

    private readonly AuthenticationRefit refit;
    #endregion

    #region [ CTors ]

    public AuthenticationService(AuthenticationRefit refit)
    {
        this.refit = refit;
    }
    #endregion

    #region [ Methods ]

    public async Task<ChangePasswordResult> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        var data = await refit.ChangePassword(new(currentPassword, newPassword));
        if (data.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return new()
            {
                IsSuccess = true
            };
        }
        else
        {
            return new()
            {
                IsSuccess = false,
                Content = data.Content
            };
        }
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var data = await refit.Login(new(email, password));
        if (data.Content is null)
            return new AuthenticationResult()
            {
                IsSuccess = false,
                Errors = new List<string>() { data.Error!.Message }
            };

        var dto = JsonConvert.DeserializeObject<POSTLoginDTO>(data.Content!.ToString()!);
        return new AuthenticationResult()
        {
            Token = dto!.accessToken,
            IsSuccess = true,
            UserInfo = new()
            {
                ID = dto!.userGuid,
            },
            RequestAt = dto!.requestAt,
            ExpiredIn = dto!.expiredIn
        };
    }
    #endregion
}
