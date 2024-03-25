using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class Details
{
    [Name("orderid")] public string OrderId { get; init; } = string.Empty;
    [Name("productid")] public string ProductId { get; init; } = string.Empty;
    [Name("unitprice")] public string UnitPrice { get; init; } = string.Empty;
    [Name("quantity")] public string Quantity { get; init; } = string.Empty;
    [Name("discount")] public string Discount { get; init; } = string.Empty;
}