using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;
public class BillServicesRepository : ApplicationBaseRepository<BillServices>, IBillServicesRepository
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public BillServicesRepository(ApplicationDbContext context) : base(context) { }
    #endregion

    #region [ Methods ]

    #endregion

}