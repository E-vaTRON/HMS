using AlohaKit.Animations;
using HMS.Domain;
using System.Collections.ObjectModel;

namespace HMS;

public partial class AnnouncementsPageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]

    private readonly IAnnouncementsPageService announcementsPageService;
    #endregion

    #region [ CTors ]

    public AnnouncementsPageViewModel(IAppNavigator appNavigator, IAnnouncementsPageService announcementsPageService) : base(appNavigator)
    {
        this.announcementsPageService = announcementsPageService;
    }
    #endregion

    #region [ Overrides ]

    protected override void OnInit(IDictionary<string, object> query)
    {
        base.OnInit(query);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        RefreshAsync(true).FireAndForget();
    }

    #endregion

    #region [ Properties ]

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    User userInfo;

    [ObservableProperty]
    ObservableCollection<AnnouncementsPageItemModel> items;

    [ObservableProperty]
    AnnouncementsPageItemModel selectedItem;
    #endregion

    #region [ Relay Commands ]

    [RelayCommand]
    async Task LaunchFileAsync(string fileUrl)
    {
        await AppNavigator.OpenUrlAsync(fileUrl);
    }

    [RelayCommand]
    async Task RefreshAsync(bool forced)
    {
        if (IsBusy)
            return;

        if (Items is null || forced)
        {
            IsBusy = true;
            var items = await announcementsPageService.GetAnnouncementItemsAsync();
            IsBusy = false;

            Items = new(items);
            if (Items.Any())
                Title = $"You have {Items.Count} announcement{(Items.Count > 1 ? "s" : string.Empty)}";
            else
                Title = "You have no announcements";
        }

        if (UserInfo is null || forced)
        {
            IsBusy = true;
            UserInfo = await announcementsPageService.GetUserInfo();
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task NavigateToProfile()
        => await AppNavigator.NavigateAsync(AppRoutes.Profile);
    #endregion
}
