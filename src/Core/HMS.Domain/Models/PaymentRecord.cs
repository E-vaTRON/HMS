namespace HMS.Domain;

public class PaymentRecord : BaseModel<string>
{
    public string Description { get; set; } = string.Empty;
    public decimal Payment { get; set; }
    public decimal UnderPaid { get; set; }
    public decimal Excess { get; set; }
    public DateTime DateOfPayment { get; set; }
    public ICollection<HMSService> Services { get; set; } = default!;
}
