namespace HMS.Contract;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(string userId);
}
