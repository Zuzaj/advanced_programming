using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class Territory
{
    [Name("territoryid")] public string TerritoryId { get; init; } = string.Empty;
    [Name("territorydescription")] public string TerritoryDescription { get; init; } = string.Empty;
    [Name("regionid")] public string RegionId { get; init; } = string.Empty;
}