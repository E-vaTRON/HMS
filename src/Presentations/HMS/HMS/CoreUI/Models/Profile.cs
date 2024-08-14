namespace HMS;

public partial class Profile : BaseUIModel
{
    [ObservableProperty]
    string firstName;
    [ObservableProperty]
    string? middleName;
    [ObservableProperty]
    string? lastName;
    [ObservableProperty]
    string? homeOwner_ID;
}
