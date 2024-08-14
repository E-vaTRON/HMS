namespace Blazor.Web;

public class MemberWithValidation : User
{
    public bool IsFirstNameValid { get; set; }
    public bool IsMiddleNameValid { get; set; } = true;
    public bool IsLastNameValid { get; set; }
    public bool IsPhoneNumberValid { get; set; }
    public bool IsEmailValid { get; set; }
}
