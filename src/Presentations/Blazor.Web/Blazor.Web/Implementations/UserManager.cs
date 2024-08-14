using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Blazor.Web;

public class UserManager : UserManager<User>
{
    #region [ CTor ]
    public UserManager(
        IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger
    ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }
    #endregion

    public async Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default!)
        => await Users.FirstOrDefaultAsync(u => u.Id == guid, cancellationToken);

    public new async Task<User?> FindByNameAsync(string userName)
    {
        var user = await base.FindByNameAsync(userName);
        return (user is null || user.IsDeleted) ? null : user;
    }

    public async Task<User?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default!)
        => await Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default!)
        => await Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null)
        => Users.WhereIf(predicate != null, predicate!);

    public async Task<List<User>> FindByGuidsAsync(string[] userGuids, CancellationToken cancellationToken = default!)
        => await Users.Where(u => userGuids.Contains(u.Id)).ToListAsync(cancellationToken);

    public async Task<string> GetResetPasswordTokenAsync(string userId)
    {
        var user = await FindByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found", userId);

        return await GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ChangePasswordWithTokenAsync(string userId, string token, string newPassword)
    {
        var user = await FindByIdAsync(userId);
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });


        var result = await ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
            return IdentityResult.Failed(result.Errors.ToArray());

        return IdentityResult.Success;
    }
}