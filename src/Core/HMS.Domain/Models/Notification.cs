namespace HMS.Domain;

public class Notification : BaseModel<string>
{
    public string Content { get; set; }
    public string IconUrl { get; set; }
    public DateTime Time { get; set; }
}
