using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blazor.Web;

public class UsersRepository : IUsersRepository
{

    #region [ Fields ]

    private readonly UserManager userManager;
    private readonly IdentityDbContext context;
    private readonly SignInManager<User> signInManager;
    #endregion

    #region [ CTors ]

    public UsersRepository(UserManager userManager,
                          SignInManager<User> signInManager,
                          IdentityDbContext context)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.context = context;
    }
    #endregion

    #region [ Methods - Reads ]

    public Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        => signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);

    public IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null)
        => userManager.FindAll(predicate);

    public Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default!)
        => userManager.FindByGuidAsync(guid, cancellationToken);

    public Task<List<User>> FindByGuidsAsync(string[] userGuids, CancellationToken cancellationToken = default!)
        => userManager.FindByGuidsAsync(userGuids, cancellationToken);

    public Task<User?> FindByNameAsync(string userName)
        => userManager.FindByNameAsync(userName);

    public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default!)
        => userManager.FindByEmailAsync(email, cancellationToken);

    public Task<User?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        => userManager.FindByPhoneNumberAsync(phoneNumber, cancellationToken);

    public async Task<User?> FindBySignalRConnectionStringId(string connectionId, CancellationToken cancellationToken = default!)
        => await userManager.FindAll(x => x.SignalRConnectionId == connectionId).FirstOrDefaultAsync(cancellationToken);

    #endregion

    #region [ Methods - Mutates ]

    public Task<IdentityResult> CreateAsync(User user, string password)
        => userManager.CreateAsync(user, password);


    public Task<IdentityResult> UpdateAsync(User user)
        => userManager.UpdateAsync(user);

    public async Task PermanentDelete(User user)
    {
        await userManager.DeleteAsync(user);
    }

    public async Task SoftDelete(User user)
    {
        user.IsDeleted = true;
        await userManager.UpdateAsync(user);
    }

    public async Task ChangePassword(User user, string currentPassword, string newPassword)
       => await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

    public async Task<string> GetResetPasswordTokenAsync(string userId)
        => await userManager.GetResetPasswordTokenAsync(userId);

    public async Task<IdentityResult> ChangePasswordWithTokenAsync(string userId, string token, string newPassword)
        => await userManager.ChangePasswordWithTokenAsync(userId, token, newPassword);

    public async Task AddRange(IEnumerable<User> users)
    {
        foreach (var user in users)
        {
            var passwordHash = userManager.PasswordHasher.HashPassword(user, MasterDataConstants.DefaultPassword);
            user.PasswordHash = passwordHash;
        }
        await context.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }

    #endregion

}
