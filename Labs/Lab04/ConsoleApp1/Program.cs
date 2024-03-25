using ConsoleApp1.Model;

namespace ConsoleApp1;

internal static class Program
{
    private static List<string> Task_2(IEnumerable<Employee> employees)
    {
        var names = employees.Select(e => e.LastName).ToList();

        foreach (var p in names)
        {
            Console.WriteLine(p);
        }
        
        return names;
    }

    private static void Task_3(IEnumerable<Employee> employees, IEnumerable<EmployeeTerritory> employeeTerritories, List<Region> regions,
        IEnumerable<Territory> territories)
    {
        if (regions == null) throw new ArgumentNullException(nameof(regions));
        var query = from employee in employees
            join employeeTerritory in employeeTerritories on employee.EmployeeId equals employeeTerritory.EmployeeId into
                empTerritoryGroup
            from etg in empTerritoryGroup.DefaultIfEmpty()
            join territory in territories on etg?.TerritoryId equals territory?.TerritoryId into territoryGroup
            from tg in territoryGroup.DefaultIfEmpty()
            join region in regions on tg?.RegionId equals region?.RegionId into regionGroup
            from rg in regionGroup.DefaultIfEmpty()
            select new
            {
                LastName = employee.LastName,
                Region = rg?.RegionDescription ?? "Unknown Region",
                Territory = tg?.TerritoryDescription ?? "Unknown Territory"
            };
        foreach (var result in query)
        {
            Console.WriteLine($"{nameof(result.LastName)}: {result.LastName}, " +
                              $"{nameof(result.Region)}: {result.Region}, " +
                              $"{nameof(result.Territory)}: {result.Territory}");
        }
    }

    private static void Task_4(IEnumerable<Employee> employees, IEnumerable<EmployeeTerritory> employeeTerritories, IEnumerable<Region> regions,
        IEnumerable<Territory> territories)
    {
        var query = from region in regions
            join territory in territories on region.RegionId equals territory.RegionId
            join employeeTerritory in employeeTerritories on territory.TerritoryId equals employeeTerritory.TerritoryId
            join employee in employees on employeeTerritory.EmployeeId equals employee.EmployeeId
            group employee by region
            into g
            select new
            {
                Region = g.Key,
                Employees = g.Select(emp => emp.LastName).Distinct().ToList()
            };

        foreach (var item in query)
        {
            Console.WriteLine($"{nameof(item.Region.RegionDescription)}: {item.Region.RegionDescription}, " +
                              $"{Environment.NewLine}" +
                              $"{nameof(item.Employees)}: {string.Join(", ", item.Employees)}" +
                              $"{Environment.NewLine}");
        }
    }

    private static void Task_5(IEnumerable<Employee> employees, IEnumerable<EmployeeTerritory> employeeTerritories, IEnumerable<Region> regions,
        IEnumerable<Territory> territories)
    {
        var query = from region in regions
            join territory in territories on region.RegionId equals territory.RegionId
            join employeeTerritory in employeeTerritories on territory.TerritoryId equals employeeTerritory.TerritoryId
            join employee in employees on employeeTerritory.EmployeeId equals employee.EmployeeId
            group employee by region
            into g
            select new
            {
                Region = g.Key,
                Employees = g.Select(emp => emp.LastName).Distinct().ToList()
            };

        foreach (var item in query)
        {
            Console.WriteLine($"{nameof(item.Region.RegionDescription)}: {item.Region.RegionDescription}, " +
                              $"{Environment.NewLine}" +
                              $"Employees count: {item.Employees.Count}" +
                              $"{Environment.NewLine}");
        }
    }

    private static void Task_6(IEnumerable<Employee> employees,IEnumerable<Order> orders, IEnumerable<Details> details){
        // var ordersByEmployee = 
        // orders.GroupJoin(details,
        //                                         order => order.OrderId,
        //                                         detail => detail.OrderId,
        //                                         (order, details) => new
        //                                         {
        //                                             EmployeeID = order.EmployeeId,
        //                                             TotalCost = details.Sum(detail => float.Parse(detail.UnitPrice) * float.Parse(detail.Quantity) * (1 - float.Parse(detail.Discount)))
        //                                         })
        //                               .GroupBy(x => x.EmployeeID)
        //                               .ToDictionary(g => g.Key, g => g.ToList());

        var ordersByEmployee = from e2 in (
                            from e in employees
                            join order in orders on e.EmployeeId equals order.EmployeeId
                            join detail in details on order.OrderId equals detail.OrderId
                            select new{
                                EmployeeID = e,
                                TotalCost = float.Parse(detail.UnitPrice)*float.Parse(detail.Quantity)*(1-float.Parse(detail.Discount))
                        
                            })
                            group e2 by e2.EmployeeID into g
                            select new{
                                Employee = g.Key,
                                Count = g.Count(),
                                Avg = g.Average(o => o.TotalCost),
                                Max = g.Max(o=>o.TotalCost)
                            };

        Console.WriteLine("EmployeeID\tOrders Count\tAverage Value\tMax Value");
        
        foreach (var item in ordersByEmployee)
        {
            Console.WriteLine($"{item.Employee.EmployeeId}\t\t{item.Count}\t\t{item.Avg}\t\t{item.Max}");
        }
    }


    private static void Main(string[] args)
    {
        var projectRoot = PathUtils.GetProjectRoot().SetAsCurrentDirectory();

        var path = Path.Combine(projectRoot, "csv_files");

        var regions = Path.Combine(path, "regions.csv").ReadCsvAsIEnumerable<Region>();
        var employees = Path.Combine(path, "employees.csv").ReadCsvAsIEnumerable<Employee>();
        var territories = Path.Combine(path, "territories.csv").ReadCsvAsIEnumerable<Territory>();
        var empTer = Path.Combine(path, "employee_territories.csv").ReadCsvAsIEnumerable<EmployeeTerritory>();
        var orders = Path.Combine(path, "orders.csv").ReadCsvAsIEnumerable<Order>();
        var details = Path.Combine(path, "orders_details.csv").ReadCsvAsIEnumerable<Details>();

        // var names = task_2(employees);
        //
        // foreach (var et in regions)
        // {
        //     Console.WriteLine(et.regionDescription);
        // }

        // task_3(employees, emp_ter, regions, territories);
         Task_4(employees, empTer, regions, territories);
        // task_5(employees, emp_ter, regions, territories);
        // task_6(employees, orders, details);
    }
}