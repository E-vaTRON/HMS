namespace HMS.LogicProvider;

public class BillingService : IBillsService
{
    #region [ CTor ]

    public BillingService()
    {

    }

    #endregion

    #region [ Properties ]

    public Task<IEnumerable<Bill>> Get()
    {
        var faker = new Faker<Bill>()
                .RuleFor(b => b.Title, f => f.PickRandom("Monthly rent", "Parking violation", "Water repair", "Electricity"))
                .RuleFor(b => b.Color, f => f.PickRandom("#c6f366", "#b687f9", "#00b985", "#fa6b72"))
                .RuleFor(b => b.FirstName, f => f.Person.FirstName)
                .RuleFor(b => b.MiddleName, f => f.Person.FirstName)
                .RuleFor(b => b.LastName, f => f.Person.LastName)
                .RuleFor(b => b.Description, f => f.Lorem.Sentence())
                .RuleFor(b => b.Amount, f => f.Finance.Amount())
                .RuleFor(b => b.TimeStamp, f => f.Date.Past(1))
                .RuleFor(b => b.DueDate, f => f.Date.Soon(30))
                .RuleFor(b => b.MonthlyDues, f => f.Finance.Amount())
                .RuleFor(b => b.Penalty, f => f.Finance.Amount());

        var fakeBillings = faker.Generate(10);
        return Task.FromResult(fakeBillings.AsEnumerable());
    }

    public Task<IEnumerable<Bill>> GetWithServices()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Bill>> GetWithServicesByUserId(string userId)
    {
        var faker = new Faker<Bill>()
                .RuleFor(b => b.Title, f => f.PickRandom("Monthly rent", "Parking violation", "Water repair", "Electricity"))
                .RuleFor(b => b.Color, f => f.PickRandom("#c6f366", "#b687f9", "#00b985", "#fa6b72"))
                .RuleFor(b => b.FirstName, f => f.Person.FirstName)
                .RuleFor(b => b.MiddleName, f => f.Person.FirstName)
                .RuleFor(b => b.LastName, f => f.Person.LastName)
                .RuleFor(b => b.Description, f => f.Lorem.Sentence())
                .RuleFor(b => b.Amount, f => f.Finance.Amount())
                .RuleFor(b => b.TimeStamp, f => f.Date.Past(1))
                .RuleFor(b => b.DueDate, f => f.Date.Soon(30))
                .RuleFor(b => b.MonthlyDues, f => f.Finance.Amount())
                .RuleFor(b => b.Penalty, f => f.Finance.Amount());

        var fakeBillings = faker.Generate(10);
        return Task.FromResult(fakeBillings.AsEnumerable());
    }

    #endregion
}
