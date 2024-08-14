namespace HMS.Contract;

public interface IUserService
{
    Task<User> GetCurrentUserAsync(string accessToken);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(string userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> UpdateUserAsync(User user);
}
