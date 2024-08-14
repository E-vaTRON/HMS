namespace Blazor.Web;

public class AddBillDialogParameter
{
    public DateTime? Deadline { get; set; }
    public DateTime? PaidDate { get; set; }

    public User SelectedUser { get; set; }
    public Request? SelectedRequest { get; set; }
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Service> ServicesData { get; set; } = default!;
    public ICollection<SymptomModel> Symptoms { get; set; } = default!; 
    public IEnumerable<Service> SelectedServices { get; set; } = [];
    public ICollection<SelectedService> SelectedServicesWithQuantity { get; set; } = [];
    public ICollection<RoomModel> Rooms { get; set; }
    public bool IsUserIdValid { get; set; }
    public bool IsDeadlineValid { get; set; }
    public List<bool>? IsRoomValid { get; set; } = new();
}

public class SelectedService : Service
{
    public int Quantity { get; set; }
    public SelectedRoom? SelectedRoom { get; set; }
    public IEnumerable<User> SelectedUsers { get; set; } = [];
}

public class SelectedRoom
{
    public string ServiceId { get; set; }
    public string RoomId { get; set; }
    public DateTime? StartDate { get; set; }
    //public DateTime? StartTime { get; set; }
    public DateTime? EndDate { get; set; }
    //public DateTime? EndTime { get; set; }
    public string? Duration { get; set; }
}