namespace HMS.LogicProvider;

public class HMSServicesService : IHMSServices
{
    #region [ Fields ]

    private readonly HMSServicesRefit refit;
    #endregion

    #region [ CTors ]

    public HMSServicesService(HMSServicesRefit refit)
    {

        this.refit = refit;

    }

    #endregion

    #region [ Methods ]

    public Task<IEnumerable<HMSService>> Get()
    {
        var data = refit.Get();
        var dto = Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<GETServiceDTO>>(data.Result.Content!.ToString()));
        List<HMSService> hmsServices = new();
        foreach (var service in dto.Result!)
        {
            hmsServices.Add(new()
            {
                ID = service.id,
                Name = service.name,
                Price = service.price,
                Color = service.color,
                CalculationType = (Domain.CalculationType)service.calculationType
            });
        }
        return Task.FromResult(hmsServices.AsEnumerable());
    }
    #endregion
}
