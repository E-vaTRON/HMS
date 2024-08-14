using System.Collections.ObjectModel;

namespace HMS;

public partial class AnnouncementsPageItemModel : BaseUIModel
{
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string? description;
    [ObservableProperty]
    string color;
    [ObservableProperty]
    string? fileUrl;
    [ObservableProperty]
    ObservableCollection<AnnouncementsPageItemKeyValueModel> items;
}
