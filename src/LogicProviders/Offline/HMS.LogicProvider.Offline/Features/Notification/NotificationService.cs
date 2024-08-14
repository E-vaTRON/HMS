namespace HMS.LogicProvider;

public class NotificationService : INotificationService
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public NotificationService()
    {

    }

    #endregion

    #region [ Methods ]

    public Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(string userId)
    {
        var notificationFaker = new Faker<Notification>()
            .RuleFor(n => n.Content, f => f.Lorem.Sentence())
            .RuleFor(n => n.IconUrl, f => f.Internet.Url())
            .RuleFor(n => n.Time, f => f.Date.Recent());

        // Generate a list of fake notifications (you can specify the number you need).
        var fakeNotifications = notificationFaker.Generate(5); // Generates 5 fake notifications.

        return Task.FromResult(fakeNotifications.AsEnumerable());
    }

    #endregion
}
