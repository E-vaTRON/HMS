namespace HMS.Contract;

public interface IAnnouncementService
{
    Task<IEnumerable<Announcement>> Get();
    Task<Announcement> GetAnnouncementAsync(string announcementId);
    Task<IEnumerable<Announcement>> GetAnnouncementsByUserIdAsync(string userId);
}
