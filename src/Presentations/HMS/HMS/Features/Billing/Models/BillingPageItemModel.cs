using System.Collections.ObjectModel;

namespace HMS;

public partial class BillingPageItemModel : BaseUIModel
{
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string? description;
    [ObservableProperty]
    string? color;
    [ObservableProperty]
    ObservableCollection<BillingPageItemKeyValueModel> items;
}
