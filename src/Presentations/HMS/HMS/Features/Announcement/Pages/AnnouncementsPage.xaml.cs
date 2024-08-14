namespace HMS;

public partial class AnnouncementsPage
{
    #region [ Fields ]

    private readonly AnnouncementsPageViewModel viewModel;
    #endregion

    #region [ CTors ]

    public AnnouncementsPage(AnnouncementsPageViewModel vm)
    {
        InitializeComponent();

        this.viewModel = vm;
        BindingContext = vm;
    }
    #endregion

    private void AnnouncementItemContentView_AnnouncementTapped(string fileUrl)
    {
        viewModel.LaunchFileCommand.Execute(fileUrl);
    }
}