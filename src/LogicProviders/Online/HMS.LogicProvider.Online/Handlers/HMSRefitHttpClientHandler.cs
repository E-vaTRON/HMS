namespace HMS.LogicProvider;

public class HMSRefitHttpClientHandler : HttpClientHandler
{
    #region [ Fields ]

    private readonly ILocalStorageService localStorageService;
    #endregion

    #region [ CTor ]

    public HMSRefitHttpClientHandler(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }
    #endregion

    #region [ Overrides ]

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        if(accessToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
    #endregion
}
