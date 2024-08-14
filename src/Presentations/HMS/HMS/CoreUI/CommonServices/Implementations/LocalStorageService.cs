
namespace HMS;

public class LocalStorageService : ILocalStorageService
{
    public async Task<string> ReadValueAsync(string key)
        => await SecureStorage.Default.GetAsync(key);

    public async Task WriteValueAsync(string key, string value)
        => await SecureStorage.Default.SetAsync(key, value);

    public void RemoveAllValue()
        => SecureStorage.Default.RemoveAll();

    public bool RemoveValue(string key)
        => SecureStorage.Default.Remove(key);
}
