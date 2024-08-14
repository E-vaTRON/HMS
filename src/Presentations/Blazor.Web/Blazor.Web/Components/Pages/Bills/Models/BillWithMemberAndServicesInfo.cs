namespace Blazor.Web;

public class BillWithMemberAndServicesInfo : Bill
{
    public User? User { get; set; } = default!;
    public Request? Request { get; set; } = default!;
    public ICollection<ServiceWithQuantity> Services { get; set; } = default!;
    public ICollection<DrugInBill> DrugInBills { get; set; } = [];
    public ICollection<BillRoom> Rooms { get; set; } = default!;
}

public class ServiceWithQuantity : Service
{
    public int Quantity { get; set; }
}

public class DrugInBill : Drug
{

}
public class BillRoom 
{
    public string Name { get; set; }
}