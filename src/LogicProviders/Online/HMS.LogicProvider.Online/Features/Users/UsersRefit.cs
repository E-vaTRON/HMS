namespace HMS.LogicProvider;

public interface UsersRefit
{
    [Get("/users")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Get();

    [Get("/users/{id}")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Get(string id);

    [Get("/users/get-current-user")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> GetCurrentUser();
}
