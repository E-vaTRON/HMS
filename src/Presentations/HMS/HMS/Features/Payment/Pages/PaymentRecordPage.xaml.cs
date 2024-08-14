namespace HMS;

public partial class PaymentRecordPage
{

    #region [ CTor ]

    public PaymentRecordPage(PaymentRecordPageViewModel vm)
    {

        InitializeComponent();

        BindingContext = vm;
    }
    #endregion

}