namespace HMS;

public interface IBillingPageService
{
    Task<IEnumerable<BillingPageItemModel>> GetAll();
}
