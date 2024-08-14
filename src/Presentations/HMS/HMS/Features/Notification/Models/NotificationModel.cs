namespace HMS;

public partial class NotificationModel : BaseUIModel
{
    [ObservableProperty]
    string content;
    [ObservableProperty]
    string iconUrl;
    [ObservableProperty]
    DateTime time;
}
