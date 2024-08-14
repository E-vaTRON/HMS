namespace HMS.LogicProvider;

public record GETUserDTO(
    string id,
    string? profileImageUrl,
    string? coverImageUrl,
    string firstName,
    string middleName,
    string lastName,
    string userName,
    string email,
    string phoneNumber,
    DateTime createdAt,
    bool? isActive,
    bool isDeleted,
    string? signalRConnectionId,
    int phase,
    int block,
    int lot,
    int lotSize,
    string street
);
