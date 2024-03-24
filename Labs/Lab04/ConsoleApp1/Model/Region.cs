namespace ConsoleApp1.Model;

public class Region
{
    public string RegionId { get; set; }
    public string RegionDescription { get; set; }

    public Region(string id, string description)
    {
        RegionId = id;
        RegionDescription = description;
    }
}