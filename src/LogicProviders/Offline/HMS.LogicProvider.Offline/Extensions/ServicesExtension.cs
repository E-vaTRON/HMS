namespace HMS.LogicProvider;

public static class ServicesExtension
{
    public static void RegisterLogicProvider(this IServiceCollection services)
    {
        services.AddTransient<IHomeService, HomeService>();
        services.AddTransient<IBillsService, BillingService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAnnouncementService, AnnouncementsService>();
        services.AddTransient<IPaymentRecordService, PaymentRecordService>();                                                     
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IUserService, UserService>();
    }
}
