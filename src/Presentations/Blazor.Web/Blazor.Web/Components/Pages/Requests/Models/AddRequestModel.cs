namespace Blazor.Web;

public class AddRequestModel
{
    public User? SelectedUser { get; set; }
    public bool IsUserIdValid { get; set; }
    public List<SymptomGroupModel> SymptomGroups { get; set; } = [];
    public List<SymptomModel> Symptoms { get; set; } = [];
    public List<SymptomModel> SelectedSymptoms { get; set; } = [];
}
