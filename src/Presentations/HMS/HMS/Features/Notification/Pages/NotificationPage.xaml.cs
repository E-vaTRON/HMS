namespace HMS;

public partial class NotificationPage
{

    #region [ CTors ]

    public NotificationPage(NotificationPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
    #endregion
}