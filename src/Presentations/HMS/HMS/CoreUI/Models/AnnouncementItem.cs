namespace HMS;

public partial class AnnouncementItem : BaseUIModel
{
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string? description;
}
