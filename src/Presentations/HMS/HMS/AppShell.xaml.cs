namespace HMS
{
    public partial class AppShell : Shell
    {
        #region [ Fields ]
        private readonly IAppInfo appInfo;

        private readonly AppSettingConfiguration appSettings;
        #endregion

        #region [ CTor ]
        public AppShell()
        {
            InitializeComponent();

            appInfo = ServiceHelper.GetService<IAppInfo>();
        }
        #endregion
    }
}
