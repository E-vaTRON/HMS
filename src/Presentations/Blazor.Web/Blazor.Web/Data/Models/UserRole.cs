using Microsoft.AspNetCore.Identity;

namespace Blazor.Web;

public class UserRole : IdentityUserRole<string>
{
    public string Id { get; set; } = default!;
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}