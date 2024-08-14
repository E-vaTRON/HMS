using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class RolesController : BaseController
{

    #region [ Fields ]

    private readonly IMapper mapper;
    private readonly RoleManager<Role> roleManager;
    #endregion

    #region [ CTors ]

    public RolesController(IMapper mapper,
                           RoleManager<Role> roleManager)
    {
        this.mapper = mapper;
        this.roleManager = roleManager;
    }
    #endregion

    #region [ GET ]

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var roles = await roleManager.Roles.ToListAsync();
        return Ok(mapper.Map<IEnumerable<RoleDTO>>(roles));
    }

    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken = default!)
    {
        var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return Ok(mapper.Map<RoleDTO>(role));
    }
    #endregion

    #region [ POST ]

    [AllowAnonymous]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RoleDTO dto)
    {
        var role = mapper.Map<Role>(dto);
        await roleManager.CreateAsync(role);

        return CreatedAtAction(nameof(Get), new { id = role.Id });
    }
    #endregion

    #region [ PUT ]

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] RoleDTO dto)
    {
        var role = await roleManager.FindByIdAsync(dto.Id);
        if (role is null)
            return NotFound();

        role.Name = dto.Name;
        role.RoleIcon = dto.RoleIcon;
        role.Summary = dto.Summary;
        role.Mission = dto.Mission;
        role.MainTasks = dto.MainTasks;


        await roleManager.UpdateAsync(role);
        return NoContent();
    }
    #endregion

    #region [ DELETE ]

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role is null)
            return NotFound();

        await roleManager.DeleteAsync(role);
        return NoContent();
    }
    #endregion


}
