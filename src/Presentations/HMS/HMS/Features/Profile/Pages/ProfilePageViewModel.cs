using HMS.Domain;

namespace HMS;

public partial class ProfilePageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]

    private readonly IUserService userService;
    private readonly ILocalStorageService localStorageService;
    private readonly IAuthenticationService authenticationService;
    #endregion

    #region [ CTors ]

    public ProfilePageViewModel(IAppNavigator appNavigator, 
                                IUserService userService,
                                ILocalStorageService localStorageService, 
                                IAuthenticationService authenticationService) : base(appNavigator)
    {
        this.userService = userService;
        this.localStorageService = localStorageService;
        this.authenticationService = authenticationService;
    }
    #endregion

    #region [ Overrides ]

    protected override async void OnInit(IDictionary<string, object> query)
    {
        base.OnInit(query);

        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        if (accessToken is null)
            return;

        CurrentUser = await userService.GetCurrentUserAsync(accessToken);
        CurrentUserFullName = CurrentUser.FirstName + CurrentUser.MiddleName + CurrentUser.LastName;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();


        if (Application.Current is not null)
        {
            IsSupportedDarkMode = true;
            IsDarkMode = Application.Current.UserAppTheme == AppTheme.Dark ? true : false;
        }
        else IsSupportedDarkMode = false;
    }

    #endregion

    #region [ Properties ]

    [ObservableProperty]
    User currentUser;

    [ObservableProperty]
    string currentUserFullName;

    [ObservableProperty]
    bool isDarkMode;

    [ObservableProperty]
    bool isSupportedDarkMode;
    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task LogoutAsync()
    {
        localStorageService.RemoveAllValue();
        await AppNavigator.NavigateAsync(AppRoutes.SignInPage);
    }

    [RelayCommand]
    async Task ChangePasswordAsync()
    {
        await AppNavigator.NavigateAsync(AppRoutes.ChangePassword);
    }
    #endregion

    #region [ Methods ]

    partial void OnIsDarkModeChanged(bool value)
    {
        Application.Current.UserAppTheme = value
                                            ? AppTheme.Dark
                                            : AppTheme.Light;

    }
    #endregion
}
