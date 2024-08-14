namespace Blazor.Web;
public class AdminWithPayment : User
{
    public bool IsFullyPaid { get; set; } = true;
    public ICollection<Bill> Bills{get; set;} = default!;
}