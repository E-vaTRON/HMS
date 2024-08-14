namespace Blazor.Web;

public record AnnouncementDTO(string? id,
                              string title,
                              string content,
                              IFormFile? file);
