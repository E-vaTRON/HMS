namespace Blazor.Web;

public record ServiceSuccess(string ServiceName = "",
                             string MethodName = "",
                             string ConsumerName = "",
                             object? AttachedData = default!) : InternalSuccess;