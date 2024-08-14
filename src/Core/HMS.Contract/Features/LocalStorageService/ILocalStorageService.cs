namespace HMS.Contract;

public interface ILocalStorageService
{
    Task WriteValueAsync(string key, string value);

    Task<string> ReadValueAsync(string key);
    bool RemoveValue(string key);
    void RemoveAllValue();
}
