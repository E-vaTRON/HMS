
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class BillsService : IBillsService
{
    #region [ Fields ]

    private readonly IUsersRepository usersRepository;
    private readonly IBillsRepository billsRepository;
    private readonly IServicesRepository servicesRepository;
    private readonly IBillServicesRepository billServicesRepository;
    #endregion

    #region [ CTors ]

    public BillsService(IUsersRepository usersRepository,
                        IBillsRepository billsRepository,
                        IServicesRepository servicesRepository,
                        IBillServicesRepository billServicesRepository)
    {
        this.usersRepository = usersRepository;
        this.billsRepository = billsRepository;
        this.servicesRepository = servicesRepository;
        this.billServicesRepository = billServicesRepository;
    }

    #endregion

    #region [ Methods ]

    public async Task<IEnumerable<BillWithServicesDTO>> GetBillWithServicesAsync(bool isDeleted = false, CancellationToken cancellationToken = default!)
    {
        var result = await billsRepository.FindAll(x => x.IsDeleted == isDeleted)
                                          .ToListAsync(cancellationToken);
        // Extract unique UserIds from the list of Bills
        var uniqueUserIds = result.Select(bill => bill.UserId).Distinct().ToList();

        // Get user information for each unique UserId
        var usersInformation = await usersRepository.FindAll(user => uniqueUserIds.Contains(user.Id) && user.IsDeleted == false)
                                                    .ToListAsync(cancellationToken);
        var billIds = result.Select(bill => bill.Id).Distinct().ToList();
        var billServices = await billServicesRepository.FindAll(x => x.IsDeleted == false && billIds.Contains(x.BillId))
                                                       .Select(x => new { x.Id, x.BillId, x.ServiceId, x.Quantity })
                                                       .ToArrayAsync(cancellationToken);
        var serviceIds = billServices.Select(x => x.ServiceId).ToList();
        var services = await servicesRepository.FindAll(x => x.IsDeleted == false && serviceIds.Contains(x.Id))
                                               .ToListAsync(cancellationToken);
        var resultWithServices = new List<BillWithServicesDTO>();
        foreach (var bill in result)
        {
            var servicesByBillId = billServices.Where(x => x.BillId == bill.Id).Select(x => x.ServiceId).ToList();
            var servicesInfo = services.Where(x => servicesByBillId.Contains(x.Id)).ToList();
            resultWithServices.Add(new()
            {
                Id = bill.Id.ToString(),
                UserId = bill.UserId.ToString(),
                LotSize = usersInformation.Where(x => x.Id == bill.UserId).Select(x => x.LotSize).FirstOrDefault(),
                Deadline = bill.Deadline,
                PaidDate = bill.PaidDate,
                ExcessAmount = bill.ExcessAmount,
                UnderPaidAmount = bill.UnderPaidAmount,
                CreatedDate = bill.CreatedDate,
                DrugIdAndQuantity = bill.DrugIdAndQuantity,
                Services = servicesInfo.Select(x => new ServiceInfoDTO
                                   (
                                    billServices.Where(y => y.BillId == bill.Id && y.ServiceId == x.Id)
                                                .Select(y => y.Quantity)
                                                .FirstOrDefault(),
                                       x.Id.ToString(),
                                       x.Name,
                                       x.Color,
                                       x.Price,
                                       (ServiceCalculationTypeDTO)x.CalculationType
                                   )).ToList()
            });
        }
        return resultWithServices;
    }

    public async Task<IEnumerable<BillWithServicesDTO>> GetBillWithServicesByUserIdAsync(string userId,
                                                                                         int lotSize,
                                                                                         bool isDeleted = false,
                                                                                         CancellationToken cancellationToken = default!)
    {
        var result = await billsRepository.FindAll(x => x.IsDeleted == isDeleted && x.UserId == userId)
                                          .ToListAsync(cancellationToken);
        var billIds = result.Select(bill => bill.Id).Distinct().ToList();
        var billServices = await billServicesRepository.FindAll(x => x.IsDeleted == false && billIds.Contains(x.BillId))
                                                       .Select(x => new { x.Id, x.BillId, x.ServiceId, x.Quantity })
                                                       .ToArrayAsync(cancellationToken);
        var serviceIds = billServices.Select(x => x.ServiceId).ToList();
        var services = await servicesRepository.FindAll(x => x.IsDeleted == false && serviceIds.Contains(x.Id))
                                               .ToListAsync(cancellationToken);
        var resultWithServices = new List<BillWithServicesDTO>();
        foreach (var bill in result)
        {
            var servicesByBillId = billServices.Where(x => x.BillId == bill.Id).Select(x => x.ServiceId).ToList();
            var servicesInfo = services.Where(x => servicesByBillId.Contains(x.Id)).ToList();
            resultWithServices.Add(new()
            {
                Id = bill.Id.ToString(),
                UserId = bill.UserId.ToString(),
                LotSize = lotSize,
                Deadline = bill.Deadline,
                PaidDate = bill.PaidDate,
                ExcessAmount = bill.ExcessAmount,
                UnderPaidAmount = bill.UnderPaidAmount,
                CreatedDate = bill.CreatedDate,
                DrugIdAndQuantity = bill.DrugIdAndQuantity,
                Services = servicesInfo.Select(x => new ServiceInfoDTO
                                   (
                                    billServices.Where(y => y.BillId == bill.Id && y.ServiceId == x.Id)
                                                .Select(y => y.Quantity)
                                                .FirstOrDefault(),
                                       x.Id.ToString(),
                                       x.Name,
                                       x.Color,
                                       x.Price,
                                       (ServiceCalculationTypeDTO)x.CalculationType
                                   )).ToList()
            });
        }
        return resultWithServices;
    }
    #endregion
}
