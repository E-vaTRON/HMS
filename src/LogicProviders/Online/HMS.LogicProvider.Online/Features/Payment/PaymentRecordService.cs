namespace HMS.LogicProvider;

public class PaymentRecordService : IPaymentRecordService
{
    #region [ Fields ]

    private readonly BillsRefit refit;
    #endregion

    #region [ CTors ]

    public PaymentRecordService(BillsRefit refit)
    {
        this.refit = refit;
    }

    #endregion

    #region [ Methods ]


    public Task<IEnumerable<PaymentRecord>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PaymentRecord> GetPaymentRecordAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PaymentRecord>> GetPaymentRecordsByUserIdAsync(string userId)
    {
        var data = await refit.GetWithServicesByUserId(userId);
        var dto = JsonConvert.DeserializeObject<IEnumerable<GETBillsWithServicesByUserIdDTO>>(data.Content!.ToString());
        List<PaymentRecord> paymentRecords = new();
        foreach (var paymentRecord in dto!.Where(x => x.paidDate is not null))
        {
            paymentRecords.Add(new()
            {
                Description = string.Empty,
                UnderPaid = paymentRecord.underPaidAmount,
                Excess = paymentRecord.excessAmount,
                Payment = PaymentRecordPaymentGenerator(paymentRecord.services, paymentRecord.lotSize),
                DateOfPayment = paymentRecord.paidDate!.Value,
                Services = paymentRecord.services.Select(x => new HMSService
                {
                    Name = x.name,
                    Color = x.color,
                    Price = x.price
                }).ToList()
            });


        }
        return paymentRecords.AsEnumerable();
    }

    #endregion

    #region [ Methods - Private ]

    private decimal PaymentRecordPaymentGenerator(GETBillServiceDTO[] services, int lotSize)
    {
        if (services == null || services.Length == 0)
            return default;

        decimal totalAmount = 0;

        foreach (var service in services)
        {
            if (service.calculationType == CalculationType.DirectAddition)
            {
                totalAmount += service.price * service.quantity;
            }
            else if (service.calculationType == CalculationType.LotSizeMultiplication)
            {
                totalAmount += service.price * lotSize;
            }
        }

        return totalAmount;
    }
    #endregion
}
