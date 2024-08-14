using Bogus;
namespace HMS.LogicProvider;

public class AnnouncementsService : IAnnouncementService
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public AnnouncementsService()
    {

    }
    #endregion

    #region [ Methods ]

    public Task<IEnumerable<Announcement>> Get()
    {
        var announcementFaker = new Faker<Announcement>()
            .RuleFor(a => a.Title, f => f.Lorem.Sentence())
            .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
            .RuleFor(a => a.PublishingDate, f => f.Date.Recent());

        var fakeAnnouncements = announcementFaker.Generate(10); 

        return Task.FromResult(fakeAnnouncements.AsEnumerable());
    }


    public Task<Announcement> GetAnnouncementAsync(string announcementId)
    {
        var announcementFaker = new Faker<Announcement>()
            .RuleFor(a => a.Title, f => f.Lorem.Sentence())
            .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
            .RuleFor(a => a.PublishingDate, f => f.Date.Recent());

        var fakeAnnouncement = announcementFaker.Generate();

        return Task.FromResult(fakeAnnouncement);
    }

    public Task<IEnumerable<Announcement>> GetAnnouncementsByUserIdAsync(string userId)
    {
        var announcementFaker = new Faker<Announcement>()
            .RuleFor(a => a.Title, f => f.Lorem.Sentence())
            .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
            .RuleFor(a => a.PublishingDate, f => f.Date.Recent());

        // Generate a list of fake announcements (you can specify the number you need).
        var fakeAnnouncements = announcementFaker.Generate(5); // Generates 5 fake announcements.

        return Task.FromResult(fakeAnnouncements.AsEnumerable());
    }
    #endregion
}
