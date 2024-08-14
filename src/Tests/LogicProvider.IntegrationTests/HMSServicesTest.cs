
namespace LogicProvider.IntegrationTests;

public class HMSServicesTest
{

    #region [ Fields ]

    private readonly IServiceProvider serviceProvider;
    #endregion

    #region [ CTor ]

    public HMSServicesTest()
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
        var hmsServicesService = serviceProvider!.GetRequiredService<IHMSServices>();
        var items = await hmsServicesService.Get();

        Assert.NotNull(items);
    }
    #endregion
}
