namespace HMS;

public partial class ChangePasswordPage
{
    #region [ Fields ]

    private readonly ChangePasswordPageViewModel viewModel;
    #endregion

    #region [ CTor ]

    public ChangePasswordPage(ChangePasswordPageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        this.BindingContext = viewModel;    
    }
    #endregion
}