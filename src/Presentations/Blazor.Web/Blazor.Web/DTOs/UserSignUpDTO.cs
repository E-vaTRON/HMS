namespace Blazor.Web;

public record UserSignUpDTO(string userName,
                            string password,
                            string firstName,
                            string? middleName,
                            string? lastName,
                            string email,
                            string phoneNumber,
                            string[]? roles,
                            int phase,
                            int lot,
                            int lotSize,
                            int block,
                            string street,
                            IFormFile? avatarFile,
                            DateTime? createdDate)

{ }