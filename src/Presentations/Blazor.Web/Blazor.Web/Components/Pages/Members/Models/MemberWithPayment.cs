namespace Blazor.Web;
public class MemberWithPayment : User
{
    public bool IsFullyPaid { get; set; } = true;
    public ICollection<Bill> Bills{get; set;} = default!;
}