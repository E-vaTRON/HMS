namespace Blazor.Web;

public class FloorModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<RoomModel> Rooms { get; set; }
}
