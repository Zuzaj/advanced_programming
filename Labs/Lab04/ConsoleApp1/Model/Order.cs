namespace ConsoleApp1.Model;

public class Order
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public string EmployeeId { get; set; }
    public string OrderDate { get; set; }
    public string RequiredDate { get; set; }
    public string ShippedDate { get; set; }
    public string ShipVisa { get; set; }
    public string Freight { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }

    public Order(string orderId, string customerId, string employeeId, string orderDate,
        string requiredDate, string shippedDate, string shipVisa, string freight, string shipName,
        string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
    {
        OrderId = orderId;
        CustomerId = customerId;
        EmployeeId = employeeId;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        ShipVisa = shipVisa;
        Freight = freight;
        ShipName = shipName;
        ShipAddress = shipAddress;
        ShipCity = shipCity;
        ShipRegion = shipRegion;
        ShipPostalCode = shipPostalCode;
        ShipCountry = shipCountry;
    }
}