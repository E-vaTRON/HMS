using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Blazor.Web;

public interface IUsersRepository
{
    #region [ READS ]
    Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default!);
    Task<List<User>> FindByGuidsAsync(string[] userGuids, CancellationToken cancellationToken = default!);
    Task<User?> FindByNameAsync(string userName);
    Task<User?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default!);
    Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null);
    Task<User?> FindBySignalRConnectionStringId(string connectionId, CancellationToken cancellationToken = default!);
    #endregion

    #region [ MUTATES ]
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> UpdateAsync(User user);
    Task AddRange(IEnumerable<User> users);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default!);
    Task PermanentDelete(User user);
    Task ChangePassword(User user, string currentPassword, string newPassword);
    Task SoftDelete(User user);
    Task<string> GetResetPasswordTokenAsync(string userId);
    Task<IdentityResult> ChangePasswordWithTokenAsync(string userId, string token, string newPassword);
    #endregion
}
