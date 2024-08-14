namespace Blazor.Web;

public class ServiceModel : Service
{
    public string UICalculationType { get; set; } = "directadd";

    public bool IsServiceNameValid { get; set; }
    public bool IsNameNotDuplicated { get; set; }
    public bool IsColorValid { get; set; }
    public bool IsPriceValid { get; set; }
}
