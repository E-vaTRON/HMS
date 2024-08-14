namespace LogicProvider.IntegrationTests;

public class UsersTest
{
    #region [ Fields ]

    private readonly IServiceProvider serviceProvider;
    #endregion

    #region [ CTor ]

    public UsersTest()
    {
        var services = new ServiceCollection();
        services.RegisterLogicProvider();
        serviceProvider = services.BuildServiceProvider();
    }
    #endregion

    #region [ Methods ]

    [Fact]
    public async Task GetTest()
    {
        var usersService = serviceProvider!.GetRequiredService<IUserService>();
        var items = await usersService.GetUsersAsync();

        Assert.NotNull(items);
    }

    [Fact]
    public async Task GetByUserIdTest()
    {
        string id = "0555ec57-ba2b-4025-9b41-1fc44ae7a28e";
        var usersService = serviceProvider!.GetRequiredService<IUserService>();
        var items = await usersService.GetUserByIdAsync(id);

        Assert.NotNull(items);
    }
    #endregion
}
