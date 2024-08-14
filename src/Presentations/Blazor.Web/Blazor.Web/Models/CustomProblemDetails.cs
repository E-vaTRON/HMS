using Microsoft.AspNetCore.Mvc;

namespace Blazor.Web;

public class CustomProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}