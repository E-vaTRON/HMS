namespace HMS;

public partial class ProfilePage
{
    #region [ CTors ]

    public ProfilePage(ProfilePageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
    #endregion
}