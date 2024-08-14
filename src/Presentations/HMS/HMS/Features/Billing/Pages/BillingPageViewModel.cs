using AlohaKit.Animations;
using System.Collections.ObjectModel;

namespace HMS;

public partial class BillingPageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]

    private readonly IBillingPageService billingPageService;
    #endregion

    #region [ CTors ]

    public BillingPageViewModel(IAppNavigator appNavigator, IBillingPageService billingPageService) : base(appNavigator)
    {
        this.billingPageService = billingPageService;
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
        RefreshAsync(true)
            .FireAndForget();
    }

    #endregion

    #region [ Properties ]

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    bool isSearchBarVisible = false;

    [ObservableProperty]
    string title = string.Empty;

    [ObservableProperty]
    ObservableCollection<BillingPageItemModel> items;

    [ObservableProperty]
    ObservableCollection<BillingPageItemModel> cachedItems;
    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task RefreshAsync(bool forced)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        var items = await billingPageService.GetAll();

        IsBusy = false;

        if (Items is null || forced)
            Items = new(items);

        if (CachedItems is null || forced)
            CachedItems = new(items);

        if (Items.Any())
            Title = $"Billing statement";
        else
            Title = "All paid up";

    }

    [RelayCommand]
    void ActivateSearch()
        => IsSearchBarVisible = true;

    [RelayCommand]
    void DisableSearch()
        => IsSearchBarVisible = false;
    #endregion

}
