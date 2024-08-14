using System.Windows.Input;

namespace HMS;

public partial class AnnouncementItemContentView : ContentView
{
    #region [ CTors ]

    public AnnouncementItemContentView()
    {
        InitializeComponent();
    }

    #endregion

    #region [ Properties ]

    public static readonly BindableProperty ComponentDataProperty = BindableProperty.Create(
        nameof(ComponentData),
        typeof(AnnouncementsPageItemModel),
        typeof(AnnouncementItemContentView),
        default(AnnouncementsPageItemModel)
    );
    public AnnouncementsPageItemModel ComponentData
    {
        get => (AnnouncementsPageItemModel)GetValue(ComponentDataProperty);
        set => SetValue(ComponentDataProperty, value);
    }

    public static readonly BindableProperty AnnouncementTappedCommandProperty = BindableProperty.Create(
        nameof(AnnouncementTappedCommand),
        typeof(ICommand),
        typeof(AnnouncementItemContentView)
    );
    public ICommand AnnouncementTappedCommand
    {
        get => (ICommand)GetValue(AnnouncementTappedCommandProperty);
        set => SetValue(AnnouncementTappedCommandProperty, value);
    }

    #endregion

    #region [ Events ]

    private void ViewFile_Clicked(object sender, EventArgs e)
    {
        AnnouncementTappedCommand?.Execute(ComponentData.FileUrl);
    }

    #endregion
}
