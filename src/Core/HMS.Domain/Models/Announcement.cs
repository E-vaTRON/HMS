namespace HMS.Domain;

public class Announcement : BaseModel<string>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public DateTime PublishingDate { get; set; }

}
