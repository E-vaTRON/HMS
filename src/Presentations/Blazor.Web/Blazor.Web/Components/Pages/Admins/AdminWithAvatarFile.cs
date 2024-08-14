﻿namespace Blazor.Web;

public class AdminWithAvatarFile : User
{
    public string[]? RolesId { get; set; }
    public string? FormPassword { get; set; }
    public IFormFile[]? AvatarFiles { get; set; }
    public bool IsFirstNameValid { get; set; }
    public bool IsMiddleNameValid { get; set; } = true;
    public bool IsLastNameValid { get; set; }
    public bool IsPhoneNumberValid { get; set; }
    public bool IsEmailValid { get; set; }
}
