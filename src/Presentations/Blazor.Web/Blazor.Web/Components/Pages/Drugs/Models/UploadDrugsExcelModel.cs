namespace Blazor.Web;

public class UploadDrugsExcelModel
{
    public IFormFile? AttachedFile { get; set; }

    public List<AddDrugModel> Drugs { get; set; } = new List<AddDrugModel>();
}
