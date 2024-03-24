namespace ConsoleApp1.Model;

public class Details
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public string UnitPrice { get; set; }
    public string Quantity { get; set; }
    public string Discount { get; set; }

    public Details(string orderId, string productId, string unitPrice, string quantity, string discount)
    {
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
    }
}