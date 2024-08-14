using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class BillsController : BaseController
{
    #region [ Fields ]

    private readonly ApplicationDbContext context;
    private readonly IBillsService billsService;
    private readonly IBillsRepository billsRepository;
    private readonly IUsersRepository usersRepository;
    private readonly IServicesRepository servicesRepository;
    private readonly IBillServicesRepository billServicesRepository;
    #endregion

    #region [ CTors ]

    public BillsController(ApplicationDbContext context,
                          IBillsService billsService,
                          IBillsRepository billsRepository,
                          IUsersRepository usersRepository,
                          IServicesRepository servicesRepository,
                          IBillServicesRepository billServicesRepository)
    {
        this.context = context;
        this.billsService = billsService;
        this.billsRepository = billsRepository;
        this.usersRepository = usersRepository;
        this.servicesRepository = servicesRepository;
        this.billServicesRepository = billServicesRepository;
    }
    #endregion

    #region [ GET ]

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default!)
    {
        var result = await billsRepository.FindAll(x => x.IsDeleted == false)
                                          .ToListAsync(cancellationToken);
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
            throw new BadRequestException("Invalid bill id format");

        var result = await billsRepository.FindByIdAsync(id, cancellationToken);
        if (result is null)
            throw new NotFoundException("Service not found", id);

        return Ok(result);
    }


    [HttpGet]
    [Route("bills-with-services")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetBillsWithServices(CancellationToken cancellationToken = default!)
    {
        var result = await billsService.GetBillWithServicesAsync(false, cancellationToken);
        return Ok(result);
    }


    [HttpGet]
    [Route("bills-with-services-by-userid/{userId}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetBillsWithServicesByUserId(string userId, CancellationToken cancellationToken = default!)
    {
        var userInfo = await this.usersRepository.FindByGuidAsync(userId, cancellationToken);
        if (userInfo is null)
            throw new NotFoundException("User not found", userId);

        var result = await billsService.GetBillWithServicesByUserIdAsync(userId, userInfo.LotSize, false, cancellationToken);
        return Ok(result);
    }
    #endregion

    #region [ POST ]

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post(BillDTO dto,
                                          CancellationToken cancellationToken = default)
    {
        var serviceIds = dto.serviceIds.Split(',').Select(x => x.Trim()).ToList();
        if (!serviceIds.Any())
            throw new BadRequestException("Invalid service ids");

        if (serviceIds.Any(id => !Guid.TryParse(id, out _)))
            throw new BadRequestException("Invalid service id found");

        if (dto.excessAmount != default(decimal) && dto.underPaidAmount != default(decimal))
            throw new BadRequestException("Only one of ExcessAmount or UnderPaidAmount should be provided");

        if ((dto.excessAmount != default(decimal) || dto.underPaidAmount != default(decimal)) && !dto.paidDate.HasValue)
            throw new BadRequestException("If ExcessAmount or UnderPaidAmount is provided, please provide the PaidDate");

        await context.Database.BeginTransactionAsync(cancellationToken);

        var services = await servicesRepository.FindAll(x => serviceIds.Contains(x.Id.ToString()))
                                               .ToListAsync(cancellationToken);

        Bill bill = new()
        {
            UserId = dto.userId,
            Deadline = dto.deadLine,
            PaidDate = dto.paidDate,
            ExcessAmount = dto.excessAmount,
            UnderPaidAmount = dto.underPaidAmount,
            CreatedDate = DateTime.UtcNow,
        };
        billsRepository.Add(bill);
        foreach (var service in services)
        {
            BillServices billServices = new()
            {
                Bill = bill,
                Service = service
            };
            billServicesRepository.Add(billServices);
        }
        await billsRepository.SaveChangesAsync(cancellationToken);
        await billServicesRepository.SaveChangesAsync(cancellationToken);

        await context.Database.CommitTransactionAsync(cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = bill.Id });
    }
    #endregion

    #region [ PUT ]

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(BillDTO dto,
                                         CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(dto.id, out _))
            throw new BadRequestException("Invalid bill id format");

        if (dto.excessAmount != default(decimal) && dto.underPaidAmount != default(decimal))
            throw new BadRequestException("Only one of ExcessAmount or UnderPaidAmount should be provided");

        if ((dto.excessAmount != default(decimal) || dto.underPaidAmount != default(decimal)) && !dto.paidDate.HasValue)
            throw new BadRequestException("If ExcessAmount or UnderPaidAmount is provided, please provide the PaidDate");

        var serviceIds = dto.serviceIds.Split(',').Select(x => x.Trim()).ToList();
        if (!serviceIds.Any())
            throw new BadRequestException("Invalid service ids");

        if (serviceIds.Any(id => !Guid.TryParse(id, out _)))
            throw new BadRequestException("Invalid service id found");

        var services = await servicesRepository.FindAll(x => serviceIds.Contains(x.Id.ToString()))
                                              .ToListAsync(cancellationToken);

        var bill = await billsRepository.FindByIdAsync(dto.id, cancellationToken);
        if (bill is null)
            throw new NotFoundException("Bill not found", dto.id);


        bill.UserId = dto.userId;
        bill.Deadline = dto.deadLine;
        bill.PaidDate = dto.paidDate;
        bill.ExcessAmount = dto.excessAmount;
        bill.UnderPaidAmount = dto.underPaidAmount;

        billsRepository.Update(bill);
        await billsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion

    #region [ DELETE ]

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id,
                                            CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(id, out _))
            throw new BadRequestException("Invalid service id format");

        var bill = await billsRepository.FindByIdAsync(id, cancellationToken);
        if (bill is null)
            throw new NotFoundException("Service not found", id);

        billsRepository.Delete(bill);
        await billsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion
}