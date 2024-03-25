using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class EmployeeTerritory
{
    [Name("employeeid")] public string EmployeeId { get; init; } = string.Empty;
    [Name("territoryid")] public string TerritoryId { get; init; } = string.Empty;
}