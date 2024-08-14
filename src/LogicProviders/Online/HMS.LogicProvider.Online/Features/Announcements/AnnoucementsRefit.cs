using Refit;

namespace HMS.LogicProvider;

public interface AnnoucementsRefit
{
    [Get("/announcements")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Get();

    [Post("/announcements")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Post();
}
