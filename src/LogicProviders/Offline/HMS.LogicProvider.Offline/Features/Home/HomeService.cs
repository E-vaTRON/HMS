namespace HMS.LogicProvider;

public class HomeService : IHomeService
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public HomeService()
    {

    }
    #endregion

    #region [ Methods ]


    public Task<IEnumerable<HomeItem>> GetAll()
    {
        return Task.FromResult(new List<HomeItem>()
        {
            new HomeItem()
            {
                ID = "1",
                Title = "Payment schedule 1",
                Type = HomeItemType.PaymentRecord,
                MonthlyDues = 1000,
                Penalty = 0,
                Amount = 0,
                Description = new Faker().Lorem.Sentence()
            },
            new HomeItem()
            {
                ID = "2",
                Title = "Announcement 1",
                Type = HomeItemType.Announcement,
                Description = new Faker().Lorem.Sentence()
            },
            new HomeItem()
            {
                ID = "3",
                Title = "Announcement 2",
                Type = HomeItemType.Announcement,
                Description = new Faker().Lorem.Sentence()
            }
        }.AsEnumerable());
    }

    public Task<IEnumerable<HomeItem>> GetAllByUserId(string userId)
    {
        return Task.FromResult(new List<HomeItem>()
        {
            new HomeItem()
            {
                ID = "1",
                Title = "Payment schedule 1",
                Type = HomeItemType.PaymentRecord,
                MonthlyDues = 1000,
                Penalty = 0,
                Amount = 0
            },
            new HomeItem()
            {
                ID = "2",
                Title = "Announcement 1",
                Type = HomeItemType.Announcement
            },
            new HomeItem()
            {
                ID = "3",
                Title = "Announcement 2",
                Type = HomeItemType.Announcement
            }
        }.AsEnumerable());
    }
    #endregion
}
