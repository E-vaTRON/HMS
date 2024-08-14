using HMS.Domain;

namespace HMS;

public interface IProfileService
{
    Task<User> GetCurrentUser();

    Task<User> GetUserByguid(string guid);

    //Task<ObservableCollection<UserPetProfile>> GetUserPetImageURL();

    Task SaveUserToLocalAsync(User user);

    Task UploadCurrentUserAvatar(FileResult file);
}