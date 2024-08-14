namespace LogicProvider.IntegrationTests;

public class AuthenticationTest
{

    #region [ Fields ]

    private readonly IServiceProvider serviceProvider;
    #endregion

    #region [ CTor ]

    public AuthenticationTest()
    {
        var services = new ServiceCollection();
        services.RegisterLogicProvider();
        serviceProvider = services.BuildServiceProvider();
    }
    #endregion

    #region [ Methods ]

    [Fact]
    public async Task LoginTest()
    {
        var email = "dummytest315";
        var password = "Welkom112!!@";
        var authenticationService = serviceProvider!.GetRequiredService<IAuthenticationService>();
        var result = await authenticationService.LoginAsync(email, password);
    }

    [Fact]
    public async Task ChangePasswordTest()
    {

    }
    #endregion
}
