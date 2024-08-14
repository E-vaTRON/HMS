using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class RequestsController : BaseController
{
    #region [ Fields ]

    private readonly ApplicationDbContext context;
    #endregion

    #region [ CTors ]

    public RequestsController(ApplicationDbContext context)
    {
        this.context = context;
    }
    #endregion

    #region [ GET ]

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default!)
    {
        var result = await context.Requests.ToListAsync(cancellationToken);
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

        var result = await context.Requests.FindAsync(id, cancellationToken);
        if (result is null)
            throw new NotFoundException("Service not found", id);

        return Ok(result);
    }
    #endregion

    #region [ POST ]

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post(RequestDTO dto,
                                          CancellationToken cancellationToken = default)
    {
        if (dto.userId is null || dto.symptoms is null)
            throw new BadRequestException("Provide all required information");

        Request request = new()
        {
            UserId = dto.userId,
            Symptoms = dto.symptoms,
        };

        context.Requests.Add(request);
        await context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = request.Id });
    }
    #endregion

    #region [ PUT ]

    #endregion

    #region [ DELETE ]

    #endregion
}
