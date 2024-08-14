using Microsoft.AspNetCore.Identity;

namespace Blazor.Web;

public class Role : IdentityRole
{
    public string? RoleIcon { get; set; }
    public string? Summary { get; set; }
    public string? Mission { get; set; }
    public string? MainTasks { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; } = new HashSet<UserRole>();
}