namespace Blazor.Web;

public class RoomModel
{
    public string Id { get; set; }
    public string FloorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsBusy { get; set; } = false;
    public ICollection<RoomEvent> Events { get; set; }
    public RoomType Type { get; set; }
}

public enum RoomType
{
    Inpatient,
    Operating,
    Isolation,
    Laboratory,
    LaborAndDelivery,
    DialysisStation,
    Consultation,
    Scan,
    Therapy,
    Recovery,
    Procedure,
    Surgery
}
