namespace Blazor.Web;

public class Bill : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string? RequestId { get; set; }
    public string DrugIdAndQuantity { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal ExcessAmount { get; set; } = default(decimal);
    public decimal UnderPaidAmount { get; set; } = default(decimal);
    public ICollection<BillServices> BillServices { get; set; } = new HashSet<BillServices>();
}
