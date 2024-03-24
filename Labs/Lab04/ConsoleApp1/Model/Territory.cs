namespace ConsoleApp1.Model;

public class Territory
{
    public string TerritoryId { get; set; }
    public string TerritoryDescription { get; set; }
    public string RegionId { get; set; }

    public Territory(string territoryId, string description, string regionId)
    {
        TerritoryId = territoryId;
        TerritoryDescription = description;
        RegionId = regionId;
    }
}