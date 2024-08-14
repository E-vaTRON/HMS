namespace HMS.LogicProvider;

public class UserService : IUserService
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public UserService()
    {

    }
    #endregion

    #region [ Methods ]


    public Task<User> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetCurrentUserAsync(string accessToken)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.ID, f => new Guid().ToString())
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.MiddleName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.UserAvatar, f => f.Person.Avatar)
            .RuleFor(u => u.HomeOwner_ID, f => f.Random.Number(10000, 99999).ToString());

        var fakeUser = userFaker.Generate();

        return Task.FromResult(fakeUser);
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(string userId)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.MiddleName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.UserAvatar, f => f.Person.Avatar)
            .RuleFor(u => u.HomeOwner_ID, f => f.Random.Number(10000, 99999).ToString());

        var fakeUser = userFaker.Generate();

        return Task.FromResult(fakeUser);

    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
    #endregion
}
