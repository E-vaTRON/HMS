using System.ComponentModel.DataAnnotations;

namespace HMS;

public partial class ChangePasswordFormModel : BaseFormModel
{
    [ObservableProperty]
    [Required(ErrorMessage = "Please enter your current password")]
    [Password(
        IncludesLower = true,
        IncludesNumber = true,
        IncludesSpecial = true,
        IncludesUpper = true,
        MinimumLength = 6,
        ErrorMessage = "Please enter a strong password: from 8 characters, 1 upper, 1 lower, 1 digit, 1 special character"
    )]
    [NotifyPropertyChangedFor(nameof(CurrentPasswordErrors))]
    [NotifyDataErrorInfo]
    string currentPassword;

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
    [DifferentFrom(nameof(CurrentPassword), ErrorMessage = "New password must be different from the current password.")]
    [NotifyPropertyChangedFor(nameof(PasswordErrors))]
    [NotifyDataErrorInfo]
    string password;

    public IEnumerable<ValidationResult> CurrentPasswordErrors => GetErrors(nameof(CurrentPassword));
    public IEnumerable<ValidationResult> PasswordErrors => GetErrors(nameof(Password));

    protected override string[] ValidatableAndSupportPropertyNames => new[]
    {
        nameof(CurrentPassword),
        nameof(CurrentPasswordErrors),
        nameof(Password),
        nameof(PasswordErrors),
    };
}