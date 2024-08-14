namespace Blazor.Web;
public interface IBillsRepository : IBaseRepository<Bill>
{
    void SoftDelete(Bill bill);
}