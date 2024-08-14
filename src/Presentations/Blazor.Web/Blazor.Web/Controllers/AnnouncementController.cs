using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class AnnouncementsController : BaseController
{
    #region [ Fields ]

    private readonly ApplicationDbContext context;
    private readonly IMediaService mediaService;
    private readonly IMediaRepository mediaRepository;
    private readonly IAnnouncementsRepository announcementsRepository;
    private readonly IServicesRepository serviceRepository;
    #endregion

    #region [ CTors ]

    public AnnouncementsController(ApplicationDbContext context,
                                   IMediaService mediaService,
                                   IMediaRepository mediaRepository,
                                   IAnnouncementsRepository announcementsRepository,
                                   IServicesRepository serviceRepository)
    {
        this.context = context;
        this.mediaService = mediaService;
        this.mediaRepository = mediaRepository;
        this.announcementsRepository = announcementsRepository;
        this.serviceRepository = serviceRepository;
    }
    #endregion

    #region [ GET ]

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default!)
    {
        var result = await announcementsRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get(string id,
                                        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(id, out _))
            throw new BadRequestException("Invalid chat message id format");

        var result = await announcementsRepository.FindByIdAsync(id, cancellationToken);
        if (result is null)
            throw new NotFoundException("Service not found", id);

        return Ok(result);
    }
    #endregion

    #region [ POST ]

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromForm] AnnouncementDTO dto,
                                          CancellationToken cancellationToken = default)
    {
        SystemMediaDTO? file = null;
        if (dto.file is not null)
            file = await mediaService.UploadFileAsync(dto.file, MediaTypeDTO.File, cancellationToken);

        Announcement announcement = new()
        {
            Content = dto.content,
            FileUrl = file?.MediaUrl,
            CreatedDate = DateTime.UtcNow
        };

        if (file is not null && file.MediaUrl is not null)
        {
            Media media = new()
            {
                MediaUrl = file.MediaUrl,
                Type = MediaType.File,
                CreatedDate = DateTime.UtcNow
            };
            mediaRepository.Add(media);
            await mediaRepository.SaveChangesAsync(cancellationToken);
        }

        announcementsRepository.Add(announcement);
        await announcementsRepository.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = announcement.Id });
    }

    #endregion

    #region [ PUT ]

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(AnnouncementDTO dto,
                                         CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(dto.id, out _))
            throw new BadRequestException("Invalid announcement id format");

        var annoucement = await announcementsRepository.FindByIdAsync(dto.id, cancellationToken);
        if (annoucement is null)
            throw new NotFoundException("Announcement not found", dto.id);

        annoucement.Content = dto.content;

        announcementsRepository.Update(annoucement);
        await announcementsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion

    #region [ DELETE ]

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id,
                                            CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(id, out _))
            throw new BadRequestException("Invalid announcement id format");

        var announcement = await announcementsRepository.FindByIdAsync(id, cancellationToken);
        if (announcement is null)
            throw new NotFoundException("Announcement not found", id);

        announcementsRepository.Delete(announcement);
        await announcementsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion
}