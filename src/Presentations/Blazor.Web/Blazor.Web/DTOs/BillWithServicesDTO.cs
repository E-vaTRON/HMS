namespace Blazor.Web;

public class BillWithServicesDTO
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string DrugIdAndQuantity { get; set; } = string.Empty;
    public int LotSize { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? PaidDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal ExcessAmount { get; set; }
    public decimal UnderPaidAmount { get; set; }
    public ICollection<ServiceInfoDTO> Services { get; set; } = new HashSet<ServiceInfoDTO>();
}

public record ServiceInfoDTO(int quantity,
                             string? id,
                             string name,
                             string color,
                             decimal price,
                             ServiceCalculationTypeDTO calculationType);