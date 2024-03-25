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
            Console.WriteLine($"Nazwisko: {result.LastName}, Region: {result.Region}, Terytorium: {result.Territory}");
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
            Console.WriteLine("Region: " + item.Region.RegionDescription);
            Console.WriteLine("Employees: " + string.Join(", ", item.Employees));
            Console.WriteLine();
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
            Console.WriteLine("Region: " + item.Region.RegionDescription);
            Console.WriteLine("Employees count: " + item.Employees.Count());
            Console.WriteLine();
        }
    }

    private static void Task_6(IEnumerable<Employee> employees, IEnumerable<Order> orders, IEnumerable<Details> details)
    {
        var ordersByEmployee = orders.GroupJoin(details,
                order => order.OrderId,
                detail => detail.OrderId,
                (order, details) => new
                {
                    EmployeeID = order.EmployeeId,
                    TotalCost = details.Sum(detail
                        => float.Parse(detail.UnitPrice) * float.Parse(detail.Quantity) * (1 - float.Parse(detail.Discount))
                    )
                }
            )
            .GroupBy(x => x.EmployeeID)
            .ToDictionary(g => g.Key, g => g.ToList());

        Console.WriteLine("EmployeeID\tOrders Count\tAverage Value\tMax Value");
        foreach (var kvp in ordersByEmployee)
        {
            var orderCount = kvp.Value.Count;
            var averageValue = kvp.Value.Average(order => order.TotalCost);
            var maxValue = kvp.Value.Max(order => order.TotalCost);

            Console.WriteLine($"{kvp.Key}\t\t{orderCount}\t\t{averageValue}\t\t{maxValue}");
        }
    }


    private static void Main(string[] args)
    {
        var regionsReader = new FileReader<Region>();
        var regions = regionsReader.ToList("csv_files/regions.csv", x => new Region(x[0], x[1]));

        var employeesReader = new FileReader<Employee>();
        var employees = employeesReader.ToList("csv_files/employees.csv", x => new Employee(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8],x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]));

        var territoryReader = new FileReader<Territory>();
        var territories = territoryReader.ToList("csv_files/territories.csv", x => new Territory(x[0], x[1], x[2]));

        var empterrReader = new FileReader<EmployeeTerritory>();
        var empTer = empterrReader.ToList("csv_files/employee_territories.csv", x => new EmployeeTerritory(x[0], x[1]));

        var orderReader = new FileReader<Order>();
        var orders = orderReader.ToList("csv_files/orders.csv", x => new Order(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13]));
        
        var detailsReader = new FileReader<Details>();
        var details = detailsReader.ToList("csv_files/orders_details.csv", x => new Details(x[0], x[1], x[2], x[3], x[4]));


        // var names = task_2(employees);
        //
        // foreach (var et in regions)
        // {
        //     Console.WriteLine(et.regionDescription);
        // }

        // task_3(employees, emp_ter, regions, territories);
        // task_4(employees, emp_ter, regions, territories);
        // task_5(employees, emp_ter, regions, territories);
        Task_6(employees, orders, details);
    }
}