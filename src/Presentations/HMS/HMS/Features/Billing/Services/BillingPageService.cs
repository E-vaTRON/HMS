using System.Globalization;

namespace HMS;

public class BillingPageService : IBillingPageService
{
    #region [ Fields ]

    private readonly IBillsService billingService;
    private readonly ILocalStorageService localStorageService;
    #endregion

    #region [ CTors ]

    public BillingPageService(IBillsService billingService, ILocalStorageService localStorageService)
    {
        this.billingService = billingService;
        this.localStorageService = localStorageService;
    }
    #endregion

    #region [ Methods ]

    public async Task<IEnumerable<BillingPageItemModel>> GetAll()
    {
        var userGuid = await localStorageService.ReadValueAsync("userguid");
        if (userGuid is null)
            return Enumerable.Empty<BillingPageItemModel>();

        var billingList = await billingService.GetWithServicesByUserId(userGuid);

        var billingPageItems = billingList.Where(x => x.PaidDate is null).Select(billing =>
        {
            BillingPageItemModel newBill = new()
            {
                Title = billing.Title,
                Description = billing.Description,
                Color = billing.Color,
                Items = new()
                    {
                        new(){ Key = "Amount", Value = billing.Amount.ToString("C2", CultureInfo.GetCultureInfo("en-PH")) },
                        new(){ Key = "Due Date", Value = billing.DueDate.ToString("dd/MM/yy") }
                    }
            };

            if (billing.MonthlyDues is not null)
                newBill.Items.Add(new() { Key = "Monthly Dues", Value = billing.MonthlyDues.ToString() });

            if (billing.Penalty is not null)
                newBill.Items.Add(new() { Key = "Penalty", Value = billing.Penalty.ToString() });

            return newBill;
        });


        return billingPageItems;
    }
    #endregion
}
