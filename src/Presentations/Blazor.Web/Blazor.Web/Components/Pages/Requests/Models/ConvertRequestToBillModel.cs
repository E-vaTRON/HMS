namespace Blazor.Web;

public class ConvertRequestToBillModel
{
    public bool IsDeadlineValid { get; set; }

    public RequestModel? Request { get; set; }
    public DateTime? Deadline { get; set; }
}
