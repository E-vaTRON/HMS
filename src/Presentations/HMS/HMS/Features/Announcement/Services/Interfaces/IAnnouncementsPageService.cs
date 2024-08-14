using HMS.Domain;

namespace HMS;

public interface IAnnouncementsPageService
{
    Task<IEnumerable<AnnouncementsPageItemModel>> GetAnnouncementItemsAsync();

    Task<IEnumerable<AnnouncementsPageItemModel>> GetHomeItemsByUserIdAsync();

    Task<User?> GetUserInfo();

}
