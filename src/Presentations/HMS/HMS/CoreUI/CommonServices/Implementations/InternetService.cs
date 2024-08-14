using SpeedTestSharp.Client;
using SpeedTestSharp.Enums;

namespace HMS;

public class InternetService : IInternetService
{
    #region [ CTor ]
    public InternetService()
    {
    }
    #endregion

    public bool IsInternetAvailable()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            return true;
        }

        return false;
    }

    public async Task<InternetSpeed> GetInternetSpeed()
    {
        var speedTestClient = new SpeedTestClient();
        var result = await speedTestClient.TestSpeedAsync(SpeedUnit.Mbps);
        return new(result.Latency, result.UploadSpeed, result.DownloadSpeed, result.SpeedUnit.ToString());
    }
}