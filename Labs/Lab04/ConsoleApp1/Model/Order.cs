using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class Order
{
    [Name("orderid")] public string OrderId { get; init; } = string.Empty;
    [Name("customerid")] public string CustomerId { get; init; } = string.Empty;
    [Name("employeeid")] public string EmployeeId { get; init; } = string.Empty;
    [Name("orderdate")] public string OrderDate { get; init; } = string.Empty;
    [Name("requireddate")] public string RequiredDate { get; init; } = string.Empty;
    [Name("shippeddate")] public string ShippedDate { get; init; } = string.Empty;
    [Name("shipvia")] public string ShipVisa { get; init; } = string.Empty;
    [Name("freight")] public string Freight { get; init; } = string.Empty;
    [Name("shipname")] public string ShipName { get; init; } = string.Empty;
    [Name("shipaddress")] public string ShipAddress { get; init; } = string.Empty;
    [Name("shipcity")] public string ShipCity { get; init; } = string.Empty;
    [Name("shipregion")] public string ShipRegion { get; init; } = string.Empty;
    [Name("shippostalcode")] public string ShipPostalCode { get; init; } = string.Empty;
    [Name("shipcountry")] public string ShipCountry { get; init; } = string.Empty;
}