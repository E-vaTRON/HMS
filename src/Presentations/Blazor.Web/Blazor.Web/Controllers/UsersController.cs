using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class UsersController : BaseController
{

    #region [ Fields ]

    private readonly IMapper mapper;
    private readonly IMediaService mediaService;
    private readonly RoleManager<Role> roleManager;
    private readonly IUsersRepository userRepository;
    private readonly IdentityDbContext identityDbContext;
    #endregion

    #region [ CTors ]

    public UsersController(IMapper mapper,
                          IMediaService mediaService,
                          RoleManager<Role> roleManager,
                          IUsersRepository userRepository,
                          IdentityDbContext identityDbContext)
    {

        this.mapper = mapper;
        this.roleManager = roleManager;
        this.mediaService = mediaService;
        this.userRepository = userRepository;
        this.identityDbContext = identityDbContext;
    }
    #endregion

    #region [ GET ]

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default!)
    {
        var result = await userRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(result);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(string id,
                                        CancellationToken cancellationToken = default!)
    {
        if (!Guid.TryParse(id, out _))
            throw new BadRequestException("Invalid chat message id format");

        var result = await userRepository.FindByGuidAsync(id, cancellationToken);
        if (result is null)
            throw new NotFoundException("User not found", id);

        return Ok(result);
    }

    [HttpGet("get-current-user")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken = default!)
    {
        var guid = HttpContext.User.FindFirst("guid")?.Value;
        if (guid is null)
            throw new BadRequestException("Please provide guid");
        var user = await userRepository.FindByGuidAsync(guid, cancellationToken);
        if (user is null)
            throw new NotFoundException("We could not find user guid in your request", guid);

        return Ok(user);
    }
    #endregion

    #region [ POST ]
    #endregion

    #region [ UPDATE ]
    #endregion

    #region [ DELETE ]

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id,
                                            CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(id, out _))
            throw new BadRequestException("Invalid user guid format");

        var user = await userRepository.FindByGuidAsync(id, cancellationToken);
        if (user is null)
            throw new NotFoundException("Service not found", id);

        await userRepository.SoftDelete(user);
        await identityDbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion
}
