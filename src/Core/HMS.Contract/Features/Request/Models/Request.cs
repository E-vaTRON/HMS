namespace HMS.Contract;

public class Request
{
    public string userId { get; set; }
    public ICollection<string> Symptoms { get; set; } = default!;
}
