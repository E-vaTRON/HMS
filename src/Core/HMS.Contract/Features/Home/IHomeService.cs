namespace HMS.Contract;

public interface IHomeService
{
    Task<IEnumerable<HomeItem>> GetAll();

    Task<IEnumerable<HomeItem>> GetAllByUserId(string userId);
}
