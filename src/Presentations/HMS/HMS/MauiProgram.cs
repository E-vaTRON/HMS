namespace HMS;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Paul-le1V.ttf", FontNames.Paulle1V);
            fonts.AddFont("FluentSystemIcons-Regular.ttf", FontNames.FluentSystemIconsRegular);
        }).UseMauiCommunityToolkitCore()
          .UseMauiCommunityToolkit()
          .RegisterServices()
          .RegisterUICoreServices()
          .RegisterUIServices()
          .RegisterVMAndPages()
          .GetAppSettings();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.RegisterLogicProvider();
        builder.Services.AddTransient<MainPage>();
        return builder.Build();
    }

    #region [ Register Services ]

    static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IProfileService, ProfileService>();
        builder.Services.AddTransient<IInternetService, InternetService>();
        return builder;
    }
    #endregion

    #region [ Register UI Core Services ]

    static MauiAppBuilder RegisterUICoreServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IAppNavigator, AppNavigator>();
        builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
        return builder;
    }
    #endregion

    #region [ Register UI Services ]

    static MauiAppBuilder RegisterUIServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IAnnouncementsPageService, AnnouncementsPageServices>();
        builder.Services.AddTransient<IBillingPageService, BillingPageService>();
        builder.Services.AddTransient<ISignInPageService, SignInPageService>();
        builder.Services.AddTransient<IPaymentPageService, PaymentPageService>();
        builder.Services.AddTransient<IChangePasswordPageService, ChangePasswordPageService>();
        builder.Services.AddTransient<IRequestService, RequestService>();
        return builder;
    }
    #endregion

    #region [ Register ViewModels and Pages ]

    static MauiAppBuilder RegisterVMAndPages(this MauiAppBuilder builder, string pattern = "Page")
    {
        var assemblies = new Assembly[] { typeof(MauiProgram).Assembly };
        var pageTypes = assemblies.SelectMany(a => a.GetTypes().Where(a => a.Name.EndsWith(pattern) && !a.IsAbstract && !a.IsInterface));
        foreach (var pageType in pageTypes)
        {
            var viewModelFullName = $"{pageType.FullName}ViewModel";
            var viewModelType = Type.GetType(viewModelFullName);

            builder.Services.AddTransient(pageType);

            if (viewModelType != null)
                builder.Services.AddTransient(viewModelType);

            Routing.RegisterRoute(pageType.FullName, pageType);
        }
        return builder;
    }
    #endregion

    #region [ Register Json Item ]
    static MauiAppBuilder GetAppSettings(this MauiAppBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("HMS.appsettings.json");
        //using var appsettingspagestream = assembly.GetManifestResourceStream("MAUIsland.Features.Gallery.Pages.BuiltIn.Helpers.AppSettingsJson.JsonFiles.appsettings.json");

        //if (stream is not null && appsettingspagestream is not null)
        if (stream is not null)
        {
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            //var appsettingsconfig = new ConfigurationBuilder()
            //            .AddJsonStream(appsettingspagestream)
            //            .Build();

            builder.Configuration.AddConfiguration(config);
            //builder.Configuration.AddConfiguration(appsettingsconfig);
        }
        else
        {
            var options = new SnackbarOptions
            {
                BackgroundColor = AppColors.Purple,
                TextColor = AppColors.White,
                ActionButtonTextColor = AppColors.Pink,
                CornerRadius = new CornerRadius(Dimensions.ButtonCornerRadius),
                CharacterSpacing = 0.5
            };
            var message = "Can't find app settings file";

            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(5), options);
            snackbar.Show();
        }

        return builder;
    }
    #endregion
}