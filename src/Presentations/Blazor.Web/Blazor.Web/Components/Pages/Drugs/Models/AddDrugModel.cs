namespace Blazor.Web;

public class AddDrugModel : Drug
{
    public IFormFile[]? DrugFiles { get; set; }
    public bool IsItemDescriptionValid { get; set; }
    public bool IsSupplierValid { get; set; }
    public bool IsQuantityValid { get; set; }
    public bool IsPriceValid { get; set; }
    public bool IsOrderDateValid { get; set; }
}
