namespace LogicProvider.IntegrationTests;

public class AnnouncementsTest
{
    #region [ Fields ]

    private readonly IServiceProvider serviceProvider;
    #endregion

    #region [ CTor ]

    public AnnouncementsTest()
    {
        var services = new ServiceCollection();
        services.RegisterLogicProvider();
        serviceProvider = services.BuildServiceProvider();
    }
    #endregion

    [Fact]
    public async Task GetTest()
    {
        var announcementService = serviceProvider!.GetRequiredService<IAnnouncementService>();
        var items = await announcementService.Get();

        Assert.NotNull(items);
    }

}