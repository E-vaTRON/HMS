namespace Blazor.Web;

public record MemberDTO(string? id,
                        string userName,
                        string password,
                        string firstName,
                        string middleName,
                        string lastName,
                        string email,
                        string phoneNumber,
                        string[]? roles,
                        int phase,
                        int lot,
                        int lotSize,
                        string street,
                        IFormFile? avatarFile)

{ }