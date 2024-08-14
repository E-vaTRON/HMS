using Blazor.Web;

public class AnnouncementModel : Announcement
{
    public IFormFile[]? AttachedFiles { get; set; }

    public bool IsTitleValid { get; set; }
    public bool IsDescriptionValid { get; set; }
}