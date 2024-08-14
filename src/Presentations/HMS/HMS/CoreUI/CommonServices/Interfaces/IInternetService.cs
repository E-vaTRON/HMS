namespace HMS;

public interface IInternetService
{
    Task<InternetSpeed> GetInternetSpeed();
    bool IsInternetAvailable();
}