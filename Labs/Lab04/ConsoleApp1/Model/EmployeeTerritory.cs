namespace ConsoleApp1.Model;

public class EmployeeTerritory
{
    public string EmployeeId { get; set; }
    public string TerritoryId { get; set; }

    public EmployeeTerritory(string employeeId, string territoryId)
    {
        EmployeeId = employeeId;
        TerritoryId = territoryId;
    }
}