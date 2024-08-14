namespace HMS;

public partial class HomeItem : BaseUIModel
{
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string? description;
    [ObservableProperty]
    string? imageUrl;
    [ObservableProperty]
    string? url;
    [ObservableProperty]
    string? notificationId;
    [ObservableProperty]
    HomeItemType type;
    [ObservableProperty]
    DateTime? publishingDate;
    [ObservableProperty]
    decimal? payment;
    [ObservableProperty]
    DateTime? dateOfPayment;
}
