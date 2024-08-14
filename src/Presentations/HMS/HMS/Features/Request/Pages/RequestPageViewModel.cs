using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace HMS;

public partial class RequestPageViewModel : NavigationAwareBaseViewModel
{
    #region [ Fields ]
    private readonly IRequestService requestService;
    private readonly ISignInPageService signInPageService;
    #endregion

    #region [ CTors ]
    public RequestPageViewModel(IAppNavigator appNavigator, 
                                IRequestService requestService, 
                                ISignInPageService signInPageService) 
        : base(appNavigator)
    {
        this.requestService = requestService;
        this.signInPageService = signInPageService;
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
        SelectedSymptoms.Clear();
        LoadData();
    }

    #endregion

    #region [ Properties ]
    [ObservableProperty]
    string title = string.Empty;

    [ObservableProperty]
    public List<SymptomGroupModel> symptomGroups = new();

    [ObservableProperty]
    public List<SymptomModel> selectedSymptoms = new();
    #endregion

    #region [ RelayCommad ]
    [RelayCommand]
    protected virtual Task GoHomeAsync() => AppNavigator.NavigateAsync(AppRoutes.Home);

    [RelayCommand]
    async Task SubmitRequest()
    {
        var currentUser = await signInPageService.GetCurrentUserAsync();
        await requestService.CreateAsync(new Request
        {
            userId = currentUser.ID,
            Symptoms = SelectedSymptoms.Select(x => x.Id).ToList(),
        });
        await AppNavigator.NavigateAsync(AppRoutes.Home);
    }
    #endregion

    public int IsExpandedConverter(bool isExpanded)
    {
        return isExpanded ? 90 : 0;
    }

    #region [ LoadData ]
    private void LoadData()
    {
        this.SymptomGroups = ServiceHelper.GetService<IConfiguration>()
                                         .GetRequiredSection("SymptomGroups")
                                         .Get<List<SymptomGroupModel>>();
    }
    #endregion
}
