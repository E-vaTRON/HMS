using System.ComponentModel.DataAnnotations;

namespace Blazor.Web;

public class RoleDTO
{
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
    public string? RoleIcon { get; set; }
    public string? Summary { get; set; }
    public string? Mission { get; set; }
    public string? MainTasks { get; set; }

}
