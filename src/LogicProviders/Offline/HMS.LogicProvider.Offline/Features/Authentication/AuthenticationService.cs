namespace HMS.LogicProvider;

public class AuthenticationService : IAuthenticationService
{
    #region [ CTor ]

    public AuthenticationService()
    {

    }

    #endregion

    #region [ Methods ]

    public Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Task.FromResult(new AuthenticationResult
            {
                IsSuccess = false,
                Errors = new List<string> { "Email or password cannot be empty" }
            });
        }
        var userFaker = new Faker<User>()
            .RuleFor(u => u.ID, f => new Guid().ToString())
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.MiddleName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.HomeOwner_ID, f => f.Random.Number(10000, 99999).ToString());

        var fakeUser = userFaker.Generate();

        var authenticationResult = new AuthenticationResult
        {
            IsSuccess = true,
            Token = "your-fake-auth-token-here", // Replace with your desired fake token.
            UserInfo = fakeUser
        };

        return Task.FromResult(authenticationResult);
    }

    public Task<AuthenticationResult> RegisterAsync(string email, string firstName, string middleName, string lastName, bool gender, string password)
    {
        throw new NotImplementedException();
    }

    public Task<ChangePasswordResult> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
            return Task.FromResult(new ChangePasswordResult
            {
                IsSuccess = false,
                Content = "Not enough information is provided"
            });

        return Task.FromResult(new ChangePasswordResult { IsSuccess = true });
    }
    #endregion
}
