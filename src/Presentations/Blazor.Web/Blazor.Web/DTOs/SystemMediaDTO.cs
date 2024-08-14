namespace Blazor.Web;

public class SystemMediaDTO
{
    public string FileName { get; set; } = default!;
    public string MediaUrl { get; set; } = default!;
    public DateTime UploadTime { get; set; }
    public MediaTypeDTO Type { get; set; }
}
