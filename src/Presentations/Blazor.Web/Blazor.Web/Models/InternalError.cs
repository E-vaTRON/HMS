namespace Blazor.Web;
public record InternalError(string ErrorMessage = "",
                            string ErrorCode = "",
                            string? SuggestionMessage = null,
                            DateTime EventOccuredAt = default!);