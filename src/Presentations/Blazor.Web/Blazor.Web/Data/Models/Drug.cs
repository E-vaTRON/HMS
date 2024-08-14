namespace Blazor.Web;

public class Drug : BaseEntity
{
    public string Supplier { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? FileReference { get; set; }
}
