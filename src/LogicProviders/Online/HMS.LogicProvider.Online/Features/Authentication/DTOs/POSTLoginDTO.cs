namespace HMS.LogicProvider;

public record POSTLoginDTO(string userGuid, 
                           DateTime requestAt, 
                           string accessToken, 
                           DateTime expiredIn);