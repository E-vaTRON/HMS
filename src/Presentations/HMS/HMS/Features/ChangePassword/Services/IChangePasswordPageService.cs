namespace HMS;

public interface IChangePasswordPageService
{
    Task<ChangePasswordResult> ChangePassword(string currentPassword, string newPassword);
    Task RemoveUserInfo();
}
