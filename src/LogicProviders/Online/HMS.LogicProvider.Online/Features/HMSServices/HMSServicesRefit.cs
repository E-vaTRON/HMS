namespace HMS.LogicProvider;

public interface HMSServicesRefit
{
    [Get("/services")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Get();
}
