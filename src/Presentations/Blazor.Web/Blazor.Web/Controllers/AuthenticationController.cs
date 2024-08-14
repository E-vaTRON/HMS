using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Web;

public class AuthenticationController : BaseController
{
    #region [ Fields ]

    private readonly IAuthenticationService _authenticationService;
    #endregion

    #region [ CTor ]

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    #endregion

    #region [ POST ]

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDTO dto, CancellationToken cancellationToken = default)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.username) || string.IsNullOrWhiteSpace(dto.password))
            return BadRequest();

        var result = await _authenticationService.Login(dto, cancellationToken);

        return result.Match(
            authenticatedInfo => (IActionResult)Ok(authenticatedInfo),
            error => NotFound(new
            {
                error.ServiceName,
                error.ErrorCode,
                error.ErrorMessage,
                error.EventOccuredAt
            })
        );
    }

    [HttpPost("login-with-phone-number")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWithPhoneNumber(PhoneNumberUserLoginDTO dto, CancellationToken cancellationToken = default)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.phoneNumber) || string.IsNullOrWhiteSpace(dto.password))
            return BadRequest();

        var result = await _authenticationService.LoginWithPhoneNumber(dto, cancellationToken);

        return result.Match(
            authenticatedInfo => (IActionResult)Ok(authenticatedInfo),
            error => NotFound(new
            {
                error.ServiceName,
                error.ErrorCode,
                error.ErrorMessage,
                error.EventOccuredAt
            })
        );
    }

    [HttpPost("login-with-email")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWithEmail(EmailUserLoginDTO dto, CancellationToken cancellationToken = default)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.email) || string.IsNullOrWhiteSpace(dto.password))
            return BadRequest();

        var result = await _authenticationService.LoginWithEmail(dto, cancellationToken);

        return result.Match(
            authenticatedInfo => (IActionResult)Ok(authenticatedInfo),
            error => NotFound(new
            {
                error.ServiceName,
                error.ErrorCode,
                error.ErrorMessage,
                error.EventOccuredAt
            })
        );
    }

    [HttpPost("register")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] UserSignUpDTO dto, CancellationToken cancellationToken = default)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.userName) || string.IsNullOrWhiteSpace(dto.password))
            return BadRequest();

        await _authenticationService.Register(dto, cancellationToken);

        return Created();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto, CancellationToken cancellationToken = default!)
    {
        var guid = HttpContext.User.FindFirst("guid")?.Value;
        if (guid is null)
            throw new BadRequestException("Please provide guid");

        if (dto == null || string.IsNullOrEmpty(dto.currentPassword) || string.IsNullOrWhiteSpace(dto.newPassword))
            throw new BadRequestException("Please provide all the required information");

        var result = await _authenticationService.ChangePassword(guid, dto, cancellationToken);

        return result.Match(
            success => (IActionResult)NoContent(),
            error => BadRequest(new
            {
                error.ServiceName,
                error.ErrorCode,
                error.ErrorMessage,
                error.EventOccuredAt
            }));
    }
    #endregion
}