using CommunityToolkit.Maui.Core.Platform;

namespace HMS;

public partial class SignInPage
{
    #region [ CTor ]

    public SignInPage(SignInPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
    #endregion


    #region [ Event Handlers ]

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        EmailEntry.CloseKeyboard();
        PasswordEntry.CloseKeyboard();
    }
    #endregion
}