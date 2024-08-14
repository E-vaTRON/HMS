namespace HMS;

public partial class PaymentRecordItem : BaseUIModel
{
    [ObservableProperty]
    string paymentRecordId;
    [ObservableProperty]
    string description;
    [ObservableProperty]
    decimal payment;
    [ObservableProperty]
    DateTime dateOfPayment;
}
