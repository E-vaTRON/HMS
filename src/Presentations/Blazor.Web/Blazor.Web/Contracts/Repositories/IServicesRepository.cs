namespace Blazor.Web;
public interface IServicesRepository : IBaseRepository<Service>
{
    Task<IEnumerable<Service>> GetByBillId(string billId);
}