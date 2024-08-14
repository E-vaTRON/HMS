using System.ComponentModel.DataAnnotations;

namespace Blazor.Web;

public class ServiceForm
{
    [Required(ErrorMessage = "Please provide a service name")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Please input a valid name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide a service price")]
    [Range(1, 100000, ErrorMessage = "Please input a valid price")]
    public decimal Price { get; set; } = default(decimal);
    public string Color { get; set; } = string.Empty;
    public CalculationType CalculationType { get; set; }
    public string UICalculationType { get; set; } = "directadd";
}
