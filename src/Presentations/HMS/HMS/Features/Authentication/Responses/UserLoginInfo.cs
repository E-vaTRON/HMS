namespace HMS;

public record UserLoginInfo(string accessToken, string userGuid, DateTime requestAt, DateTime expireIn);
