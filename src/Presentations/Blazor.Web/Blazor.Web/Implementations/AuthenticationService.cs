using AutoMapper;
using CommunityToolkit.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Blazor.Web;

public class AuthenticationService : IAuthenticationService
{

    #region [ Fields ]

    private readonly IMapper mapper;
    private readonly IMediaService mediaService;
    private readonly RoleManager<Role> roleManager;
    private readonly IUsersRepository userRepository;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IdentityDbContext identityDbContext;
    #endregion

    #region [ CTors ]

    public AuthenticationService(IMapper mapper,
                                 IMediaService mediaService,
                                 RoleManager<Role> roleManager,
                                 IUsersRepository userRepository,
                                 IJwtTokenService jwtTokenService,
                                 IdentityDbContext identityDbContext)
    {

        this.mapper = mapper;
        this.roleManager = roleManager;
        this.mediaService = mediaService;
        this.userRepository = userRepository;
        this.jwtTokenService = jwtTokenService;
        this.identityDbContext = identityDbContext;
    }
    #endregion

    #region [ Methods ]

    public async Task<OneOf<AuthenticatedResponseDTO, ServiceError>> Login(UserLoginDTO dto, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(dto);

        var user = await userRepository.FindByNameAsync(dto.username);
        if (user is null)
            return new ServiceError(nameof(AuthenticationService), nameof(Login))
            {
                ErrorMessage = ServiceConstants.USER_NOT_FOUND,
                ErrorCode = nameof(ServiceConstants.USER_NOT_FOUND),
                EventOccuredAt = DateTime.Now
            };

        var passwordCheck = await userRepository.CheckPasswordSignInAsync(user, dto.password, false);
        if (!passwordCheck.Succeeded)
            return new ServiceError(nameof(AuthenticationService), nameof(Login))
            {
                ErrorMessage = ServiceConstants.PASSWORD_INVALID,
                ErrorCode = nameof(ServiceConstants.PASSWORD_INVALID),
                EventOccuredAt = DateTime.Now
            };

        var requestAt = DateTime.UtcNow;
        var expiredIn = requestAt.AddDays(1);
        var accessToken = jwtTokenService.GenerateToken(user, requestAt, expiredIn);

        return new AuthenticatedResponseDTO(user.Id, requestAt, accessToken, expiredIn);
    }

    public async Task<OneOf<AuthenticatedResponseDTO, ServiceError>> LoginWithPhoneNumber(PhoneNumberUserLoginDTO dto, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(dto);

        var user = await userRepository.FindByPhoneNumberAsync(dto.phoneNumber);
        if (user is null)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.USER_NOT_FOUND,
                ErrorCode = nameof(ServiceConstants.USER_NOT_FOUND),
                EventOccuredAt = DateTime.Now
            };
        var passwordCheck = await userRepository.CheckPasswordSignInAsync(user, dto.password, false);
        if (!passwordCheck.Succeeded)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.PASSWORD_INVALID,
                ErrorCode = nameof(ServiceConstants.PASSWORD_INVALID),
                EventOccuredAt = DateTime.Now
            };
        var requestAt = DateTime.UtcNow;
        var expiredIn = requestAt.AddDays(1);
        var accessToken = jwtTokenService.GenerateToken(user, requestAt, expiredIn);

        return new AuthenticatedResponseDTO(user.Id, requestAt, accessToken, expiredIn);
    }

    public async Task<OneOf<AuthenticatedResponseDTO, ServiceError>> LoginWithEmail(EmailUserLoginDTO dto, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(dto);

        var user = await userRepository.FindByEmailAsync(dto.email, cancellationToken);
        if (user is null)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.USER_NOT_FOUND,
                ErrorCode = nameof(ServiceConstants.USER_NOT_FOUND),
                EventOccuredAt = DateTime.Now
            };
        var passwordCheck = await userRepository.CheckPasswordSignInAsync(user, dto.password, false);
        if (!passwordCheck.Succeeded)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.PASSWORD_INVALID,
                ErrorCode = nameof(ServiceConstants.PASSWORD_INVALID),
                EventOccuredAt = DateTime.Now
            };
        var requestAt = DateTime.UtcNow;
        var expiredIn = requestAt.AddDays(1);
        var accessToken = jwtTokenService.GenerateToken(user, requestAt, expiredIn);

        return new AuthenticatedResponseDTO(user.Id, requestAt, accessToken, expiredIn);
    }
    public async Task<OneOf<AuthenticatedResponseDTO, ServiceError>> AdminLoginWithEmail(EmailUserLoginDTO dto, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(dto);


        var user = await userRepository.FindByEmailAsync(dto.email, cancellationToken);
        if (user is null)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.USER_NOT_FOUND,
                ErrorCode = nameof(ServiceConstants.USER_NOT_FOUND),
                EventOccuredAt = DateTime.Now
            };

        var userRole = await identityDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == MasterDataConstants.AdminRoleId);
        if(userRole is null)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.USER_IS_NOT_ADMIN,
                ErrorCode = nameof(ServiceConstants.USER_IS_NOT_ADMIN),
                EventOccuredAt = DateTime.Now
            };

        var passwordCheck = await userRepository.CheckPasswordSignInAsync(user, dto.password, false);
        if (!passwordCheck.Succeeded)
            return new ServiceError(nameof(AuthenticationService), nameof(LoginWithPhoneNumber))
            {
                ErrorMessage = ServiceConstants.PASSWORD_INVALID,
                ErrorCode = nameof(ServiceConstants.PASSWORD_INVALID),
                EventOccuredAt = DateTime.Now
            };
        var requestAt = DateTime.UtcNow;
        var expiredIn = requestAt.AddDays(1);
        var accessToken = jwtTokenService.GenerateToken(user, requestAt, expiredIn);

        return new AuthenticatedResponseDTO(user.Id, requestAt, accessToken, expiredIn);
    }

    public async Task Register(UserSignUpDTO dto, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(dto);

        var user = mapper.Map<User>(dto);

        if (dto.createdDate is null)
            user.CreatedAt = DateTime.UtcNow;

        var result = await userRepository.CreateAsync(user, dto.password);

        if (!result.Succeeded)
            return;

        if (dto.avatarFile is null)
            return;

        var uploadedAvatarInfomation = await mediaService.UploadFileAsync(dto.avatarFile,
                                                                            MediaTypeDTO.Avatar,
                                                                            cancellationToken);
        user.ProfileImageUrl = uploadedAvatarInfomation.MediaUrl;
        await userRepository.UpdateAsync(user);
    }

    public async Task<OneOf<ServiceSuccess, ServiceError>> ChangePassword(string userId, ChangePasswordDTO dto, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByGuidAsync(userId, cancellationToken);
        if (user is null)
            return new ServiceError(nameof(AuthenticationService), nameof(ChangePassword))
            {
                ErrorMessage = ServiceConstants.USER_NOT_FOUND,
                ErrorCode = nameof(ServiceConstants.USER_NOT_FOUND),
                EventOccuredAt = DateTime.Now
            };

        var oldPasswordCheck = await userRepository.CheckPasswordSignInAsync(user, dto.currentPassword, false);
        if (!oldPasswordCheck.Succeeded)
            return new ServiceError(nameof(AuthenticationService), nameof(ChangePassword))
            {
                ErrorMessage = ServiceConstants.PASSWORD_INVALID,
                ErrorCode = nameof(ServiceConstants.PASSWORD_INVALID),
                EventOccuredAt = DateTime.Now
            };

        await userRepository.ChangePassword(user, dto.currentPassword, dto.newPassword);
        return new ServiceSuccess(nameof(AuthenticationService), nameof(ChangePassword))
        {
            SuccessMessage = ServiceConstants.USER_CHANGED_PASSWORD_SUCCESSFULLY,
            SuccessCode = nameof(ServiceConstants.USER_CHANGED_PASSWORD_SUCCESSFULLY),
            EventOccuredAt = DateTime.Now
        };

    }

    #endregion
}
