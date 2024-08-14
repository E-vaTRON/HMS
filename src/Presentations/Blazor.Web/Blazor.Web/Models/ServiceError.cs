namespace Blazor.Web;


public record ServiceError(string ServiceName = "", string MethodName = "", string ConsumerName = "") : InternalError;