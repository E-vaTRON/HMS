using HMS.Domain;

namespace HMS;

public partial class SignInPageViewModel : BaseViewModel
{
    #region [ Fields ]

    private readonly ISignInPageService signInPageService;
    #endregion

    #region [ CTors ]

    public SignInPageViewModel(IAppNavigator appNavigator,
                               ISignInPageService signInPageService) : base(appNavigator)
    {
        this.signInPageService = signInPageService;

        Form = new();
    }
    #endregion

    #region [ Properties ]

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    bool isShakeAnimation;

    public SignInFormModel Form { get; init; }

    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task SignInAsync()
    {
        IsBusy = true;
        var isValid = Form.IsValid();

        if (!isValid)
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync("Please input valid credentials");
            return;
        }


        if (!signInPageService.IsInternetAvailable())
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync("Internet is not available");
            return;
        }

        var result = await signInPageService.LoginAsync(Form.Email, Form.Password);

        if (result is null)
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync("Invalid credentials");
            return;
        }

        await signInPageService.SaveUserAccessTokenAsync(result.accessToken);
        await signInPageService.SaveUserGuidAsync(result.userGuid);

        var userInfo = await signInPageService.GetCurrentUserAsync();

        if (userInfo is null)
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync("User does not exist");
            return;
        }

        IsBusy = false;
        await GoHomeAsync(userInfo);
    }

    [RelayCommand]
    async Task SignUpAsync()
    {

    }

    [RelayCommand]
    async Task ForgotPasswordAsync()
    {

    }
    #endregion

    #region [ Methods - Private ]

    Task GoHomeAsync(User userProfile)
    {
        //Guard.IsNotNull(userProfile);
        return AppNavigator.NavigateAsync(AppRoutes.Profile, true, userProfile);
    }
    #endregion
}
