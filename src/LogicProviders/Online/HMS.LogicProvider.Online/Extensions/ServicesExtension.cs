namespace HMS.LogicProvider;

public static class ServicesExtension
{
    public static void RegisterLogicProvider(this IServiceCollection services)
    {
        //string baseUrl = "https://HMShoai.azurewebsites.net/api";
        //string baseUrl = "https://xs5tn1vh-7192.asse.devtunnels.ms/api";
        string baseUrl = "https://kwfj0fw0-7192.asse.devtunnels.ms/api";
        //string baseUrl = "https://localhost:7192/api";

        services.AddTransient<HMSRefitHttpClientHandler>();

        services.AddRefitClient<AuthenticationRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<AnnoucementsRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>() 
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<BillsRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<UsersRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<HMSServicesRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<RequestRefit>()
                .ConfigurePrimaryHttpMessageHandler<HMSRefitHttpClientHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IBillsService, BillsService>();
        services.AddTransient<IHMSServices, HMSServicesService>();
        services.AddTransient<IAnnouncementService, AnnouncementsService>();
        services.AddTransient<IPaymentRecordService, PaymentRecordService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IRequestService, RequestService>();
    }
}
