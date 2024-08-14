namespace HMS.Domain;

public class Bill : BaseModel<string>
{
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public string Color { get; set; } = "#c6f366";
    public decimal Amount { get; set; }
    public DateTime? PaidDate { get; set; }
    public DateTime? TimeStamp { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? MonthlyDues { get; set; }
    public decimal? Penalty { get; set; }
}
