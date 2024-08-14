namespace LogicProvider.IntegrationTests;

public class BillsTest
{

    #region [ Fields ]

    private readonly IServiceProvider serviceProvider;
    #endregion

    #region [ CTor ]

    public BillsTest()
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
        var billsService = serviceProvider!.GetRequiredService<IBillsService>();
        var items = await billsService.Get();

        Assert.NotNull(items);
    }

    [Fact]
    public async Task GetWithServiceTest()
    {
        var billsServices = serviceProvider!.GetRequiredService<IBillsService>();
        var items = await billsServices.GetWithServices();

        Assert.NotNull(items);
    }
    #endregion
}
