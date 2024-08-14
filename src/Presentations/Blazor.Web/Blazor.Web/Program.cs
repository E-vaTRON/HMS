var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidIssuer = builder.Configuration["JwtTokenConfig:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenConfig:Key"]!)),
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    };
                });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddUserManager<UserManager>()
    .AddDefaultTokenProviders();

builder.Services.AddBlazorBootstrap();
builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig"));
builder.Services.Configure<JwtTokenConfig>(builder.Configuration.GetSection("JwtTokenConfig"));
builder.Services.Configure<List<FloorModel>>(builder.Configuration.GetSection("Floors"));
builder.Services.Configure<List<SymptomGroupModel>>(builder.Configuration.GetSection("SymptomGroups"));
builder.Services.AddDbContextPool<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton((provider) =>
{
    var config = provider.GetRequiredService<IOptionsMonitor<AzureStorageConfig>>().CurrentValue;
    return new StorageSharedKeyCredential(config.AccountName, config.AccountKey);
});
builder.Services.AddTransient<Func<AzureBlobContainerEnum, BlobContainerClient>>(provider => container =>
{
    var config = provider.GetRequiredService<IOptionsMonitor<AzureStorageConfig>>().CurrentValue;
    switch (container)
    {
        case AzureBlobContainerEnum.Avatar:
            return new BlobContainerClient(config.BlobConnectionString, config.AvatarFiles);

        case AzureBlobContainerEnum.General:
            return new BlobContainerClient(config.BlobConnectionString, config.GeneralFiles);

        default:
            return new BlobContainerClient(config.BlobConnectionString, config.GeneralFiles);
    }
});
builder.Services.AddTransient<IUsersRepository, UsersRepository>()
                .AddTransient<IMediaRepository, MediaRepository>()
                .AddTransient<IBillsRepository, BillsRepository>()
                .AddTransient<IServicesRepository, ServicesRepository>()
                .AddTransient<IDrugRepository, DrugRepository>()
                .AddTransient<IBillServicesRepository, BillServicesRepository>()
                .AddTransient<IAnnouncementsRepository, AnnouncementsRepository>()
                .AddTransient<IBillsService, BillsService>()
                .AddTransient<IRequestRepository, RequestRepository>()
                .AddTransient<IJwtTokenService, JwtTokenService>()
                .AddTransient<IMediaService, AzureBlobService>()
                .AddTransient<IExcelService, ExcelService>()
                .AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new MappingConfig())).CreateMapper());
builder.Services.AddFluentUIComponents();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
