namespace Blazor.Web;

public class RequestModel : Request
{
    public User UserInfo { get; set; }
    public ICollection<SymptomModel> SymptomModels { get; set; } = default!;
}
