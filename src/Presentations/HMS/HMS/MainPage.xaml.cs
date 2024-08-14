namespace HMS
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IAnnouncementsPageService _homePageService;
        private readonly IAnnouncementService _announcementService;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;


        public MainPage(IAnnouncementsPageService homePageService,
                        IAnnouncementService announcementService,
                        IPaymentRecordService paymentRecordService,
                        INotificationService notificationService,
                        IUserService userService)
        {
            InitializeComponent();
            _homePageService = homePageService;
            _announcementService = announcementService;
            _paymentRecordService = paymentRecordService;
            _notificationService = notificationService;
            _userService = userService;

            var result = _homePageService.GetAnnouncementItemsAsync().GetAwaiter().GetResult();
            var result1 = _announcementService.GetAnnouncementsByUserIdAsync("!23").GetAwaiter().GetResult();
            var result2 = _paymentRecordService.GetPaymentRecordsByUserIdAsync("123").GetAwaiter().GetResult();
            var result3 = _notificationService.GetNotificationsByUserIdAsync("123").GetAwaiter().GetResult();
            var result4 = _userService.GetUserByIdAsync("123").GetAwaiter().GetResult();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
