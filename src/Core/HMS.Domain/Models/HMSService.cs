namespace HMS.Domain;

public class HMSService : BaseModel<string>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public CalculationType CalculationType { get; set; }
    public bool IsDeleted { get; set; }
}
