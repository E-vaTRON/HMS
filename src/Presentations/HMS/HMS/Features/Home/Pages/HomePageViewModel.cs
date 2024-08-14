namespace HMS;

public partial class HomePageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]

    private readonly IAuthenticationService authenticationService;
    #endregion

    #region [ CTors ]

    public HomePageViewModel(IAppNavigator appNavigator, IAuthenticationService authenticationService) : base(appNavigator)
    {
        this.authenticationService = authenticationService;
    }
    #endregion

    #region [ Properties ]

    [ObservableProperty]
    string announcementsRoute = AppRoutes.Announcements;

    [ObservableProperty]
    string paymentRecordRoute = AppRoutes.PaymentRecord;

    [ObservableProperty]
    string profileRoute = AppRoutes.Profile;

    [ObservableProperty]
    string billingRoute = AppRoutes.Billing;

    [ObservableProperty]
    string requestRoute = AppRoutes.Request;
    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task GoToRouteAsync(string route)
        => await AppNavigator.NavigateAsync(route);
    #endregion
}
