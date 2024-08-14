namespace HMS.Contract;

public class AuthenticationResult
{
    public string Token { get; set; }
    public bool IsSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public User UserInfo { get; set; }
    public DateTime RequestAt { get; set; }
    public DateTime ExpiredIn { get; set; }
}
