namespace Blazor.Web;


public interface IJwtTokenService
{
    string GenerateToken(User user, DateTime iat, DateTime exp);
}
