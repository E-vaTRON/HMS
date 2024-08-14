using System.Collections.ObjectModel;

namespace HMS;

public partial class PaymentRecordPageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]

    private readonly IPaymentPageService paymentPageService;
    #endregion

    #region [ CTors ]

    public PaymentRecordPageViewModel(IAppNavigator appNavigator, IPaymentPageService paymentPageService) : base(appNavigator)
    {
        this.paymentPageService = paymentPageService;
    }
    #endregion

    #region [ Overrides ]

    protected override void OnInit(IDictionary<string, object> query)
    {
        base.OnInit(query);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        await RefreshAsync(true);
    }

    #endregion

    #region [ Properties ]

    [ObservableProperty]
    string title = "Payment record";

    [ObservableProperty]
    ObservableCollection<PaymentModel> items;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    PaymentModel selectedItem;
    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task RefreshAsync(bool forced)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        var items = await paymentPageService.GetPaymentRecordsAsync();

        IsBusy = false;

        if (Items is null || forced)
            Items = new(items);

    }
    #endregion
}
