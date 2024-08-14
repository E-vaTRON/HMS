namespace HMS.Contract;

public interface IBillsService
{
    Task<IEnumerable<Bill>> Get();

    Task<IEnumerable<Bill>> GetWithServices();

    Task<IEnumerable<Bill>> GetWithServicesByUserId(string userId);
}
