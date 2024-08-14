namespace HMS.LogicProvider;

public class UserService : IUserService
{
    #region [ Fields ]

    private readonly UsersRefit refit;
    #endregion

    #region [ CTors ]

    public UserService(UsersRefit refit)
    {
        this.refit = refit;
    }
    #endregion

    #region [ Methods ]

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        var data = refit.Get();
        var dto = Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<GETUserDTO>>(data.Result.Content!.ToString()));
        List<User> users = new();
        foreach (var item in dto.Result!)
        {
            users.Add(new()
            {
                ID = item.id,
                FirstName = item.firstName,
                MiddleName = item.middleName,
                LastName = item.lastName,
                UserAvatar = item.profileImageUrl
            });
        }
        return Task.FromResult(users.AsEnumerable());
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(string userId)
    {
        var data = refit.Get(userId);
        var dto = Task.FromResult(JsonConvert.DeserializeObject<GETUserDTO>(data.Result.Content!.ToString())).Result;
        return Task.FromResult(new User()
        {
            ID = dto!.id,
            FirstName = dto.firstName,
            MiddleName = dto.middleName,
            LastName = dto.lastName,
            UserAvatar = dto.profileImageUrl
        });
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetCurrentUserAsync(string accessToken)
    {
        var data = await refit.GetCurrentUser();
        var dto = JsonConvert.DeserializeObject<GETUserDTO>(data.Content!.ToString());
        return new User()
        {
            ID = dto!.id,
            FirstName = dto.firstName,
            MiddleName = dto.middleName,
            LastName = dto.lastName,
            UserAvatar = dto.profileImageUrl,
            PhoneNumber = dto.phoneNumber,
            Email = dto.email,
            Street = dto.street
        };
    }
    #endregion
}
