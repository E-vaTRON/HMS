using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Blazor.Web;

public class JwtTokenService : IJwtTokenService
{

    #region [ Fields ]

    private readonly UserManager _userManager;
    private readonly JwtTokenConfig _tokenConfig;
    #endregion

    #region [ CTors ]

    public JwtTokenService(UserManager userManager,
                           IOptionsMonitor<JwtTokenConfig> tokenConfigOptionsAccessor)
    {

        _userManager = userManager;
        _tokenConfig = tokenConfigOptionsAccessor.CurrentValue;
    }
    #endregion

    #region [ Methods ]

    public string GenerateToken(User user, DateTime iat, DateTime exp)
    {
        var handler = new JwtSecurityTokenHandler();

        var identity = new ClaimsIdentity(
            new GenericIdentity(user.UserName ?? user.Email, "TokenAuth"),
            new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("guid", user.Id)
                  }
            // TODO: add more user's claims
            );

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfig.Issuer,
            Audience = "hms-clients",  // TODO: client's name/id
            SigningCredentials = creds,
            Subject = identity,
            IssuedAt = iat,
            Expires = exp
        });

        return handler.WriteToken(securityToken);
    }
    #endregion

}
