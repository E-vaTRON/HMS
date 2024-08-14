namespace HMS;

public class SymptomGroupModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<SymptomModel> Symptoms { get; set; }
}
