namespace HMS.LogicProvider.Online;

public class BillsService : IBillsService
{
    #region [ Fields ]

    private readonly BillsRefit refit;
    #endregion

    #region [ CTors ]

    public BillsService(BillsRefit refit)
    {

        this.refit = refit;

    }
    #endregion

    #region [ Methods ]

    public async Task<IEnumerable<Bill>> Get()
    {
        var data = await refit.Get();
        var dto = JsonConvert.DeserializeObject<IEnumerable<GETBillsDTO>>(data.Content!.ToString());
        List<Bill> bills = new();
        foreach (var bill in dto!)
        {
            bills.Add(new()
            {
                ID = bill.id,
                DueDate = bill.deadline,
                
            });
        }
        return bills.AsEnumerable();
    }

    public async Task<IEnumerable<Bill>> GetWithServices()
    {
        var data = await refit.GetWithServices();
        var dto = JsonConvert.DeserializeObject<IEnumerable<GETBillsWithServicesDTO>>(data.Content!.ToString());  
        List<Bill> bills = new();
        foreach (var bill in dto!)
        {
            bills.Add(new()
            {
                ID = bill.id,
                Title = BillTitleGenerator(bill.services),
                Color = BillColorGenerator(bill.services),
                DueDate = bill.deadline,

            });
        }
        return bills.AsEnumerable();
    }

    public async Task<IEnumerable<Bill>> GetWithServicesByUserId(string userId)
    {
        var data = await refit.GetWithServicesByUserId(userId);
        var dto = JsonConvert.DeserializeObject<IEnumerable<GETBillsWithServicesByUserIdDTO>>(data.Content!.ToString());
        List<Bill> bills = new();
        foreach (var bill in dto!)
        {
            Bill newbill = new()
            {
                ID = bill.id,
                Title = BillTitleGenerator(bill.services),
                Color = BillColorGenerator(bill.services),
                Amount = BillAmountGenerator(bill.services, bill.lotSize),
                DueDate = bill.deadline,
                PaidDate = bill.paidDate
            };

            var monthlyDueService = bill.services.FirstOrDefault(x => x.name.Equals("Monthly Due", StringComparison.OrdinalIgnoreCase));
            if (monthlyDueService is not null)
                newbill.MonthlyDues = monthlyDueService.price;

            var penaltyService = bill.services.FirstOrDefault(x => x.name.Equals("Penalty", StringComparison.OrdinalIgnoreCase));
            if (penaltyService is not null)
                newbill.Penalty = penaltyService.price;

            bills.Add(newbill);

        }
        return bills.AsEnumerable();
    }
    #endregion

    #region [ Methods - Private ]

    string BillTitleGenerator(GETBillServiceDTO[] services)
    {
        if (services == null || services.Length == 0)
            return string.Empty;

        var titleBuilder = new StringBuilder();

        foreach (var service in services)
        {
            titleBuilder.Append(service.name);

            if (service != services.Last())
            {
                titleBuilder.Append(" + ");
            }
        }

        return titleBuilder.ToString();
    }

    string BillColorGenerator(GETBillServiceDTO[] services)
    {
        if (services == null || services.Length == 0)
            return string.Empty;

        // Find the service with the highest price
        var maxPriceService = services.OrderByDescending(s => s.price).First();

        return maxPriceService.color;
    }

    decimal BillAmountGenerator(GETBillServiceDTO[] services, int lotSize)
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
