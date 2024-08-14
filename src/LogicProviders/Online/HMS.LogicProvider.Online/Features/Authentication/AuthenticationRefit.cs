using Refit;

namespace HMS.LogicProvider;

public interface AuthenticationRefit
{

    [Post("/authentication/login-with-email")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Login(LoginParameter parameter);

    [Put("/authentication/change-password")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> ChangePassword(ChangePasswordParameter parameter);
}
