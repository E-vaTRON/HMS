using CommunityToolkit.Diagnostics;
using HMS.Domain;

namespace HMS;

public class ProfileService : IProfileService
{
    #region [ Fields ]
    private readonly IAppNavigator appNavigator;
    private readonly ILocalStorageService localStorageService;
    private readonly IUserService userService;
    #endregion

    #region [ CTor ]
    public ProfileService(IAppNavigator appNavigator,
                          ILocalStorageService localStorageService,
                          IUserService userService)
    {
        this.appNavigator = appNavigator;
        this.localStorageService = localStorageService;
        this.userService = userService;
    }
    #endregion

    #region [ Services ]

    public async Task<User> GetCurrentUser()
    {
        var accessToken = await localStorageService.ReadValueAsync("accesstoken");
        Guard.IsNotNullOrEmpty(accessToken);

        var userInfo = await userService.GetCurrentUserAsync(accessToken);
        Guard.IsNotNull(userInfo);

        return new()
        {
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            MiddleName = userInfo.MiddleName,
            HomeOwner_ID = userInfo.HomeOwner_ID
        };
    }

    public Task<User> GetUserByguid(string guid)
    {
        throw new NotImplementedException();
    }

    public Task SaveUserToLocalAsync(User user)
    {
        return Task.Run(() => null);
    }

    public async Task UploadCurrentUserAvatar(FileResult file)
    {
        Guard.IsNotNull(file);

        var accessToken = await localStorageService.ReadValueAsync("accesstoken");

        Guard.IsNotNullOrEmpty(accessToken);

        using var fileStream = File.OpenRead(file.FullPath);

        //var stream = new StreamPart(fileStream, file.FileName, file.ContentType);

        //try
        //{
        //    var result = await userService.UpdateAvatar(stream);
        //    if (result.IsSuccessStatusCode)
        //    {
        //        await appNavigator.ShowSnackbarAsync("Save success !!!");
        //    }
        //    else
        //    {
        //        await appNavigator.ShowSnackbarAsync($"Something wrong !!! {result.StatusCode}");
        //    }
        //}
        //catch (ApiException ex)
        //{
        //    throw;
        //}
    }
    #endregion
}