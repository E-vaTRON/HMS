namespace HMS;

public partial class PaymentModel : BaseUIModel
{
    [ObservableProperty]
    string payment;
    [ObservableProperty]
    DateTime dateOfPayment;
    [ObservableProperty]
    string paid;
    [ObservableProperty]
    string balance;
    [ObservableProperty]
    ICollection<ItemDescription> items;
}
