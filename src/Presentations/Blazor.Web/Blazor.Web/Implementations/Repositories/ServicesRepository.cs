
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class ServicesRepository : ApplicationBaseRepository<Service>, IServicesRepository
{
    #region [ Fields ]

    private readonly IBillServicesRepository billServicesRepository;
    #endregion

    #region [ CTors ]

    public ServicesRepository(ApplicationDbContext context,
                              IBillServicesRepository billServicesRepository) : base(context)
    {
        this.billServicesRepository = billServicesRepository;
    }

    #endregion

    #region [ Methods ]

    public async Task<IEnumerable<Service>> GetByBillId(string billId)
    {
        var billServices = await billServicesRepository.FindAll(x => x.BillId == new Guid(billId))
                                                            .Select(x => x.ServiceId)
                                                       .ToListAsync();
        return await FindAll(x => billServices.Contains(x.Id))
                                            .ToListAsync();
    }
    #endregion

}