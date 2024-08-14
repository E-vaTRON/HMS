using CommunityToolkit.Diagnostics;

namespace HMS;

public partial class ChangePasswordPageViewModel : NavigationAwareBaseViewModel
{

    #region [ Fields ]

    private readonly IChangePasswordPageService changePasswordPageService;
    #endregion

    #region [ CTors ]

    public ChangePasswordPageViewModel(IAppNavigator appNavigator, IChangePasswordPageService changePasswordPageService) : base(appNavigator)
    {
        Form = new();

        this.changePasswordPageService = changePasswordPageService;
    }
    #endregion

    #region [ Overrides ]


    #endregion

    #region [ Properties ]

    [ObservableProperty]
    bool isBusy;

    public ChangePasswordFormModel Form { get; init; }
    #endregion

    #region [ Relay commands ]

    [RelayCommand]
    async Task ChangePasswordAsync()
    {
        IsBusy = true;

        if (!Form.IsValid())
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync("Please check both of your password again");
            return;
        }

        Guard.IsNotNullOrEmpty(Form.CurrentPassword);
        Guard.IsNotNullOrEmpty(Form.Password);

        var result = await changePasswordPageService.ChangePassword(Form.CurrentPassword, Form.Password);
        if (!result.IsSuccess)
        {
            IsBusy = false;
            await AppNavigator.ShowSnackbarAsync(result.Detail, null, "Try again");
        }
        else
        {
            IsBusy = false;
            await changePasswordPageService.RemoveUserInfo();
            await AppNavigator.ShowSnackbarAsync("Change password success", null, "Done");
            await AppNavigator.NavigateAsync(AppRoutes.SignInPage);
        }
    }
    #endregion

    #region [ Methods ]

    #endregion
}
