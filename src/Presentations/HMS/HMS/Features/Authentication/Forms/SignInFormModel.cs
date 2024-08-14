using System.ComponentModel.DataAnnotations;

namespace HMS;

public partial class SignInFormModel : BaseFormModel
{
    [ObservableProperty]
    [Required(ErrorMessage = "Please enter your email or user name")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [NotifyPropertyChangedFor(nameof(EmailErrors))]
    [NotifyDataErrorInfo]
    //string email;
    string email = "asramos@gmail.com";

    [ObservableProperty]
    [Required(ErrorMessage = "Please enter a password")]
    [Password(
        IncludesLower = true,
        IncludesNumber = true,
        IncludesSpecial = true,
        IncludesUpper = true,
        MinimumLength = 6,
        ErrorMessage = "Please enter a strong password: from 8 characters, 1 upper, 1 lower, 1 digit, 1 special character"
    )]
    [NotifyPropertyChangedFor(nameof(PasswordErrors))]
    [NotifyDataErrorInfo]
    //string password;
    string password = "Welkom112!!@";

    public IEnumerable<ValidationResult> EmailErrors => GetErrors(nameof(Email));
    public IEnumerable<ValidationResult> PasswordErrors => GetErrors(nameof(Password));

    protected override string[] ValidatableAndSupportPropertyNames => new[]
    {
        nameof(Email),
        nameof(EmailErrors),
        nameof(Password),
        nameof(PasswordErrors),
    };
}