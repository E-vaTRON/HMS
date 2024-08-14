namespace HMS.Domain;

public class User : BaseModel<string>
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? HomeOwner_ID { get; set; }
    public string? UserAvatar { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Street { get; set; }
}
