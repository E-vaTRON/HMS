namespace Blazor.Web;

public record FileUploadedInformation(string blobGuid,
                                      string name,
                                      string absoluteUri,
                                      MediaTypeDTO mediaType,
                                      DateTime uploadDate);