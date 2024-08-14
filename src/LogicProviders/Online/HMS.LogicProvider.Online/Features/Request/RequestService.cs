namespace HMS.LogicProvider;

public class RequestService : IRequestService
{
    #region [ Fields ]

    private readonly RequestRefit refit;
    #endregion

    #region [ CTors ]

    public RequestService(RequestRefit refit)
    {
        this.refit = refit;
    }
    #endregion

    #region [ Methods ]
    public async Task CreateAsync(Request request)
    { 
        await refit.Post(new RequestDTO
        {
            userId = request.userId,
            Symptoms = string.Join(", ", request.Symptoms)
        });
        return;
    }
    #endregion

    #region [ Methods - Private ]
    #endregion
}
