namespace HMS.Contract;

public interface IRequestService
{
    Task CreateAsync(Request request);
}
