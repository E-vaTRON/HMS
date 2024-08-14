namespace HMS.Contract;

public interface IPaymentRecordService
{
    Task<IEnumerable<PaymentRecord>> GetAll();
    Task<PaymentRecord> GetPaymentRecordAsync();
    Task<IEnumerable<PaymentRecord>> GetPaymentRecordsByUserIdAsync(string userId);
}
