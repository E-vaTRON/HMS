namespace Blazor.Web;

public class BillServices : BaseEntity
{
    public Guid BillId { get; set; }
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; }
    public virtual Service? Service { get; set; }
    public virtual Bill? Bill { get; set; }
}
