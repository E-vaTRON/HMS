namespace Blazor.Web;

public class RequestRepository : ApplicationBaseRepository<Request>, IRequestRepository
{
    public RequestRepository(ApplicationDbContext context) : base(context)
    {
    }
}
