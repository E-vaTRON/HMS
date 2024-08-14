namespace Blazor.Web;

public record InternalSuccess(string SuccessMessage = "",
                              string SuccessCode = "",
                              DateTime EventOccuredAt = default!);