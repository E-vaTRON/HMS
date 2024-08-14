namespace Blazor.Web;

public class Media : BaseEntity
{
    public string MediaName { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public DateTime TimeUpload { get; set; } = DateTime.UtcNow;
    public MediaType Type { get; set; }
}
public enum MediaType
{
    Video, Photo, UserAvatar, Audio, File, Other
}

