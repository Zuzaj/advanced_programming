using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class Region
{
    [Name("regionid")] public string RegionId { get; init; } = string.Empty;
    [Name("regiondescription")] public string RegionDescription { get; init; } = string.Empty;
}