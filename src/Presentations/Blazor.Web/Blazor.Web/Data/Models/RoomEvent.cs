namespace Blazor.Web;

public class RoomEvent : BaseEntity
{
    public string RoomId { get; set; }
    public string BillId { get; set; }
    public string ServiceId { get; set; }
    public string? UserIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
