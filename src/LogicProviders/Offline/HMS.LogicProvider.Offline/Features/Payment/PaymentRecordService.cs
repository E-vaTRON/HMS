using Bogus;
namespace HMS.LogicProvider;

public class PaymentRecordService : IPaymentRecordService
{
    #region [ Fields ]

    #endregion

    #region [ CTors ]

    public PaymentRecordService()
    {

    }


    #endregion

    #region [ Methods ]
    public Task<IEnumerable<PaymentRecord>> GetAll()
    {
        // Simulate fetching a list of fake payment records for a user with Bogus.
        var paymentRecordFaker = new Faker<PaymentRecord>()
            .RuleFor(pr => pr.Description, f => f.Lorem.Sentence())
            .RuleFor(pr => pr.Payment, f => f.Finance.Amount())
            .RuleFor(pr => pr.DateOfPayment, f => f.Date.Past());

        // Generate a list of fake payment records (you can specify the number you need).
        var fakePaymentRecords = paymentRecordFaker.Generate(15); // Generates 5 fake payment records.

        return Task.FromResult(fakePaymentRecords.AsEnumerable());
    }

    public Task<PaymentRecord> GetPaymentRecordAsync()
    {
        // Simulate fetching a fake payment record with Bogus.
        var paymentRecordFaker = new Faker<PaymentRecord>()
            .RuleFor(pr => pr.Description, f => f.Lorem.Sentence())
            .RuleFor(pr => pr.Payment, f => f.Finance.Amount())
            .RuleFor(pr => pr.DateOfPayment, f => f.Date.Past());

        var fakePaymentRecord = paymentRecordFaker.Generate();

        return Task.FromResult(fakePaymentRecord);
    }

    public Task<IEnumerable<PaymentRecord>> GetPaymentRecordsByUserIdAsync(string userId)
    {
        // Simulate fetching a list of fake payment records for a user with Bogus.
        var paymentRecordFaker = new Faker<PaymentRecord>()
            .RuleFor(pr => pr.Description, f => f.Lorem.Sentence())
            .RuleFor(pr => pr.Payment, f => f.Finance.Amount())
            .RuleFor(pr => pr.DateOfPayment, f => f.Date.Past());

        // Generate a list of fake payment records (you can specify the number you need).
        var fakePaymentRecords = paymentRecordFaker.Generate(5); // Generates 5 fake payment records.

        return Task.FromResult(fakePaymentRecords.AsEnumerable());
    }

    #endregion
}
