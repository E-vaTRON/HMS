namespace HMS;

public interface IPaymentPageService
{
    Task<IEnumerable<PaymentModel>> GetPaymentRecordsAsync();
}
