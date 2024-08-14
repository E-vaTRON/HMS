namespace Blazor.Web;

public class RoomInfoDetailModel
{
    public string Name { get; set; } = string.Empty;
    public bool IsBusy { get; set; }
    public ICollection<RoomEventDialog> Events { get; set; } = new List<RoomEventDialog>();
}

public class RoomEventDialog : RoomEvent
{
    public ICollection<User> Participants { get; set; } = new List<User>();
    public string EventTitle { get; set; }
}
