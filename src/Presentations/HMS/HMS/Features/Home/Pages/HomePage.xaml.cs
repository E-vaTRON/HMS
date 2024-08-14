namespace HMS;

public partial class HomePage
{
    #region [ CTors ]

    public HomePage(HomePageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
    #endregion
}