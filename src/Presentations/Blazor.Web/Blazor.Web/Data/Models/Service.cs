
namespace Blazor.Web;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = default(decimal);
    public string Color { get; set; } = string.Empty;
    public CalculationType CalculationType { get; set; }
    public ICollection<BillServices> BillServices { get; set; } = new HashSet<BillServices>();
}

public enum CalculationType
{
    DirectAddition,
    LotSizeMultiplication,
}

