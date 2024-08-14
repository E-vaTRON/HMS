using HMS.Domain;

namespace HMS;

public class AnnouncementsPageServices : IAnnouncementsPageService
{
    #region [ Fields ]

    private readonly IUserService userService;
    private readonly IAnnouncementService announcementService;
    private readonly ILocalStorageService localStorageService;
    #endregion

    #region [ CTor ]

    public AnnouncementsPageServices(IUserService userService,
                                     IAnnouncementService announcementService,
                                     ILocalStorageService localStorageService)
    {
        this.userService = userService;
        this.announcementService = announcementService;
        this.localStorageService = localStorageService;
    }
    #endregion

    #region [ Methods ]


    public async Task<IEnumerable<AnnouncementsPageItemModel>> GetAnnouncementItemsAsync()
    {
        var items = await announcementService.Get();
        var annoucements = items.Select(x => new AnnouncementsPageItemModel
        {
            Title = x.Title,
            Description = x.Description,
            Id = x.ID,
            FileUrl = x.FileUrl
        });
        return annoucements;
    }

    public async Task<IEnumerable<AnnouncementsPageItemModel>> GetHomeItemsByUserIdAsync()
    {
        //var userId = await localStorageService.ReadValueAsync("userId");
        //if (string.IsNullOrEmpty(userId))
        //    return Enumerable.Empty<AnnouncementsPageItemModel>();

        //var homeItems = await announcementService.(userId);
        //var homePageItems = homeItems.Select(x => new AnnouncementsPageItemModel
        //{
        //    Title = x.Title,
        //    Description = x.Description,
        //    Id = x.ID
        //});
        //return homePageItems;
        return Enumerable.Empty<AnnouncementsPageItemModel>();
    }

    public async Task<User?> GetUserInfo()
    {
        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        if (accessToken is null)
            return null;

        return await userService.GetCurrentUserAsync(accessToken);

    }
    #endregion
}
