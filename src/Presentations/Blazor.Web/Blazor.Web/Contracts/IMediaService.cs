namespace Blazor.Web;

public interface IMediaService
{
    Task<SystemMediaDTO> UploadFileAsync(IFormFile file, MediaTypeDTO type, CancellationToken cancellationToken = default!);

    Task DeleteFileAsync(string fileName, bool deleteSnapShot, CancellationToken cancellationToken = default!);
}
