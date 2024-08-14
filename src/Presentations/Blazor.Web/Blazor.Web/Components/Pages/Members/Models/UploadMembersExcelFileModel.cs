namespace Blazor.Web;

public class UploadMembersExcelFileModel
{
    public bool IsContentValid { get; set; }
    public IFormFile? AttachedFile { get; set; }
    public IEnumerable<MemberWithValidation> Users { get; set; } = new List<MemberWithValidation>();
}
