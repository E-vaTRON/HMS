namespace HMS.Domain;

public class HomeItem : BaseModel<string>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Url { get; set; }
    public string? NotificationId { get; set; }
    public DateTime? PublishingDate { get; set; }
    public decimal? Payment { get; set; }
    public HomeItemType Type { get; set; }

    public decimal MonthlyDues { get; set; }
    public decimal Penalty { get; set; }
    public decimal Amount { get; set; }
    public DateTime? DueDate { get; set; }

}
