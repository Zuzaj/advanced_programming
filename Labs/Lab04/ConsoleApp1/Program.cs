using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1{


class Program{

    static List<string> task_2(List<Employee> employees){
        var names = from e in employees
                    select e.lastName;
        foreach (var p in names.ToList())
            Console.WriteLine(p);
        return names.ToList();
    }

    static void task_3(List<Employee> employees,List<EmployeeTerritory> employeeTerritories, List<Region> regions, List<Territory> territories){
        var query = from employee in employees
                    join employeeTerritory in employeeTerritories on employee.employeeId equals employeeTerritory.employeeId into empTerritoryGroup
                    from etg in empTerritoryGroup.DefaultIfEmpty()
                    join territory in territories on etg?.territoryId equals territory?.territoryId into territoryGroup
                    from tg in territoryGroup.DefaultIfEmpty()
                    join region in regions on tg?.regionId equals region?.regionId into regionGroup
                    from rg in regionGroup.DefaultIfEmpty()
                    select new
                    {
                        LastName = employee.lastName,
                        Region = rg?.regionDescription ?? "Unknown Region",
                        Territory = tg?.territoryDescription ?? "Unknown Territory"
                    };
        foreach (var result in query)
        {
            Console.WriteLine($"Nazwisko: {result.LastName}, Region: {result.Region}, Terytorium: {result.Territory}");
        }
    }

    static void task_4(List<Employee> employees,List<EmployeeTerritory> employeeTerritories, List<Region> regions, List<Territory> territories){
     
        var query = from region in regions
                    join territory in territories on region.regionId equals territory.regionId
                    join employeeTerritory in employeeTerritories on territory.territoryId equals employeeTerritory.territoryId
                    join employee in employees on employeeTerritory.employeeId equals employee.employeeId
                    group employee by region into g
                    select new
                    {
                        Region = g.Key,
                        Employees = g.Select(emp => emp.lastName).Distinct().ToList()
                    };

        foreach (var item in query)
        {
            Console.WriteLine("Region: " + item.Region.regionDescription);
            Console.WriteLine("Employees: " + string.Join(", ", item.Employees));
            Console.WriteLine();
        }
    }

    static void task_5(List<Employee> employees,List<EmployeeTerritory> employeeTerritories, List<Region> regions, List<Territory> territories){
     
        var query = from region in regions
                    join territory in territories on region.regionId equals territory.regionId
                    join employeeTerritory in employeeTerritories on territory.territoryId equals employeeTerritory.territoryId
                    join employee in employees on employeeTerritory.employeeId equals employee.employeeId
                    group employee by region into g
                    select new
                    {
                        Region = g.Key,
                        Employees = g.Select(emp => emp.lastName).Distinct().ToList()
                    };

        foreach (var item in query)
        {
            Console.WriteLine("Region: " + item.Region.regionDescription);
            Console.WriteLine("Employees count: " + item.Employees.Count());
            Console.WriteLine();
        }
    }

    static void task_6(List<Employee> employees,List<Order> orders, List<Details> details){
        var ordersByEmployee = orders.GroupJoin(details,
                                                order => order.orderId,
                                                detail => detail.orderId,
                                                (order, details) => new
                                                {
                                                    EmployeeID = order.employeeId,
                                                    TotalCost = details.Sum(detail => float.Parse(detail.unitPrice) * float.Parse(detail.quantity) * (1 - float.Parse(detail.discount)))
                                                })
                                      .GroupBy(x => x.EmployeeID)
                                      .ToDictionary(g => g.Key, g => g.ToList());

        Console.WriteLine("EmployeeID\tOrders Count\tAverage Value\tMax Value");
        foreach (var kvp in ordersByEmployee)
        {
            int orderCount = kvp.Value.Count;
            float averageValue = kvp.Value.Average(order => order.TotalCost);
            float maxValue = kvp.Value.Max(order => order.TotalCost);

            Console.WriteLine($"{kvp.Key}\t\t{orderCount}\t\t{averageValue}\t\t{maxValue}");
        }
    }


    static void Main(string[] args){

    FileReader<Region> regions_reader = new FileReader<Region>();
    List<Region> regions = regions_reader.toList("csv_files/regions.csv", x => new Region(x[0], x[1]));

    FileReader<Employee> employees_reader = new FileReader<Employee>();
    List<Employee> employees = employees_reader.toList("csv_files/employees.csv", x => new Employee(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], 
                                                                                            x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]));

    FileReader<Territory> territory_reader = new FileReader<Territory>();
    List<Territory> territories = territory_reader.toList("csv_files/territories.csv", x => new Territory(x[0], x[1], x[2]));

    FileReader<EmployeeTerritory> empterr_reader = new FileReader<EmployeeTerritory>();
    List<EmployeeTerritory> emp_ter = empterr_reader.toList("csv_files/employee_territories.csv", x => new EmployeeTerritory(x[0], x[1]));

    FileReader<Order> order_reader = new FileReader<Order>();
    List<Order> orders = order_reader.toList("csv_files/orders.csv", x => new Order(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], 
                                                                                            x[9], x[10], x[11], x[12], x[13]));
    FileReader<Details> details_reader = new FileReader<Details>();
    List<Details> details = details_reader.toList("csv_files/orders_details.csv", x => new Details(x[0], x[1], x[2], x[3], x[4]));
    


    //var names = task_2(employees);

    // foreach ( var et in regions){
    //     Console.WriteLine(et.regionDescription);
    // }

    //task_3(employees, emp_ter, regions, territories);
    //task_4(employees, emp_ter, regions, territories);
    //task_5(employees, emp_ter, regions, territories);
    task_6(employees, orders, details);


}

}
}