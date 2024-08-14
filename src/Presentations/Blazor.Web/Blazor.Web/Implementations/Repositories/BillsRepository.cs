using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class BillsRepository : ApplicationBaseRepository<Bill>, IBillsRepository
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public BillsRepository(ApplicationDbContext context) : base(context) { }
    #endregion

    #region [ Methods ]

    public override async Task<Bill?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        => await _dbSet.Include(x => x.BillServices)
                                    .ThenInclude(x => x.Service)
                                 .FirstOrDefaultAsync(x => x.Id == new Guid(id));

    public void SoftDelete(Bill bill)
    {
        bill.IsDeleted = true;
        _dbSet.Update(bill);
        _context.SaveChanges();
    }
    #endregion

}
