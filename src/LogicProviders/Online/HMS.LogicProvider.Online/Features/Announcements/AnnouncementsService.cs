namespace HMS.LogicProvider;

public class AnnouncementsService : IAnnouncementService
{
    #region [ Fields ]

    private readonly AnnoucementsRefit refit;
    #endregion

    #region [ CTors ]

    public AnnouncementsService(AnnoucementsRefit refit)
    {
        this.refit = refit;
    }
    #endregion

    #region [ Methods ]

    public async Task<IEnumerable<Announcement>> Get()
    {
        var data = await refit.Get();
        var dto = JsonConvert.DeserializeObject<IEnumerable<GETAnnouncementsDTO>>(data.Content!.ToString());
        List<Announcement> announcements = new();
        foreach (var item in dto!)
        {
            announcements.Add(new()
            {
                ID = item.id,
                Title = item.title,
                Description = item.content,
                PublishingDate = item.createdDate,
                FileUrl = item.fileUrl

            });
        }
        return announcements.AsEnumerable();
    }

    public Task<Announcement> GetAnnouncementAsync(string announcementId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> GetAnnouncementsByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }
    #endregion
}
