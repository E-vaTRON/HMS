using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;


public class ServicesController : BaseController
{
    #region [ Fields ]

    private readonly ApplicationDbContext context;
    private readonly IServicesRepository serviceRepository;
    #endregion

    #region [ CTor ]

    public ServicesController(ApplicationDbContext context,
                              IServicesRepository serviceRepository)
    {
        this.context = context;
        this.serviceRepository = serviceRepository;
    }
    #endregion

    #region [ GET ]

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await serviceRepository.FindAll().ToListAsync();
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
            throw new BadRequestException("Invalid service id format");

        var result = await serviceRepository.FindByIdAsync(id, cancellationToken);
        if (result is null)
            throw new NotFoundException("Service not found", id);

        return Ok(result);
    }
    #endregion

    #region [ POST ]

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post(ServiceDTO dto,
                                          CancellationToken cancellationToken = default)
    {
        Service service = new()
        {
            Name = dto.name,
            Price = dto.price,
            Color = dto.color,
            CalculationType = (CalculationType)dto.calculationType,
            CreatedDate = DateTime.Now
        };

        serviceRepository.Add(service);
        await serviceRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = service.Id });
    }
    #endregion

    #region [ PUT ]

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ServiceDTO dto,
                                         CancellationToken cancellationToken = default)
    {


        if (!Guid.TryParse(dto.id, out _))
            throw new BadRequestException("Invalid service id format");

        var service = await serviceRepository.FindByIdAsync(dto.id, cancellationToken);
        if (service is null)
            throw new NotFoundException("Service not found", dto.id);

        service.Name = dto.name;
        service.Price = dto.price;
        service.Color = dto.color;
        service.CalculationType = (CalculationType)dto.calculationType;

        serviceRepository.Update(service);
        await serviceRepository.SaveChangesAsync(cancellationToken);
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
            throw new BadRequestException("Invalid service id format");

        var service = await serviceRepository.FindByIdAsync(id, cancellationToken);
        if (service is null)
            throw new NotFoundException("Service not found", id);

        serviceRepository.Delete(service);
        await serviceRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion
}