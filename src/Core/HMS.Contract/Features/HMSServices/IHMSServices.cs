
namespace HMS.Contract;

public interface IHMSServices
{
    Task<IEnumerable<HMSService>> Get();
}
