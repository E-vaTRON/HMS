namespace Blazor.Web;

public class EditBillDialogParameter : BillWithMemberAndServicesInfo
{
    public ICollection<SymptomModel> Symptoms { get; set; } = default!;
    public ICollection<Drug> Drugs { get; set; } = default!;
    public IEnumerable<Drug> SelectedDrugs { get; set; } = [];
    public IEnumerable<DrugInBill> SelectedDrugInBills { get; set; } = [];
}
