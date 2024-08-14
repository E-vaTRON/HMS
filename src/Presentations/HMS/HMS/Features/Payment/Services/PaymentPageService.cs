using System.Globalization;

namespace HMS;

public class PaymentPageService : IPaymentPageService
{
    #region [ Fields ]

    private readonly ILocalStorageService localStorageService;
    private readonly IPaymentRecordService paymentRecordService;
    #endregion

    #region [ CTor ]

    public PaymentPageService(ILocalStorageService localStorageService,
                              IPaymentRecordService paymentRecordService)
    {
        this.localStorageService = localStorageService;
        this.paymentRecordService = paymentRecordService;
    }
    #endregion

    #region [ Methods ]


    public async Task<IEnumerable<PaymentModel>> GetPaymentRecordsAsync()
    {
        List<PaymentModel> result = new();

        var userGuid = await localStorageService.ReadValueAsync("userguid");
        if (userGuid is null)
            return Enumerable.Empty<PaymentModel>();

        var paymentRecord = await paymentRecordService.GetPaymentRecordsByUserIdAsync(userGuid);
        foreach (var record in paymentRecord)
        {
            result.Add(new PaymentModel
            {
                Paid = (record.Payment + record.Excess - record.UnderPaid).ToString("C2", CultureInfo.GetCultureInfo("en-PH")),
                Balance = record.UnderPaid.ToString("C2", CultureInfo.GetCultureInfo("en-PH")),
                Payment = record.Payment.ToString("C2", CultureInfo.GetCultureInfo("en-PH")),
                DateOfPayment = record.DateOfPayment,
                Items = record.Services.Select(x => new ItemDescription()
                {
                    Name = x.Name,
                    Color = x.Color
                }).ToList()
            });
        }
        return result;
    }
    #endregion
}
