namespace HMS.LogicProvider;

public record GETServiceDTO(
    string name,
    decimal price,
    string color,
    int calculationType,
    string id,
    DateTime createdDate,
    bool isDeleted
);
