using Refit;

namespace HMS.LogicProvider;

public interface RequestRefit
{
    [Post("/requests")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Post([Body] RequestDTO request);
}

