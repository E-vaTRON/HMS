namespace HMS;

public partial class NotificationPageViewModel : BaseViewModel
{
    #region [ Fields ]

    private readonly IAuthenticationService authenticationService;
    #endregion

    #region [ CTors ]

    public NotificationPageViewModel(IAppNavigator appNavigator, IAuthenticationService authenticationService) : base(appNavigator)
    {
        this.authenticationService = authenticationService;
    }
    #endregion

    #region [ Properties ]

    #endregion
}
