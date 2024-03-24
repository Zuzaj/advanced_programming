namespace ConsoleApp1;

public class Region
{
    public string RegionId { get; set; }
    public string RegionDescription { get; set; }

    public Region(string id, string description)
    {
        RegionId = id;
        RegionDescription = description;
    }
}

public class Territory
{
    public string TerritoryId { get; set; }
    public string TerritoryDescription { get; set; }
    public string RegionId { get; set; }

    public Territory(string territoryId, string description, string regionId)
    {
        TerritoryId = territoryId;
        TerritoryDescription = description;
        RegionId = regionId;
    }
}

public class Employee
{
    public string EmployeeId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Title { get; set; }
    public string Courtesy { get; set; }
    public string Birthdate { get; set; }
    public string HireDate { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string HomePhone { get; set; }
    public string Extension { get; set; }
    public string Photo { get; set; }
    public string Notes { get; set; }
    public string ReportsTo { get; set; }
    public string PhotoPath { get; set; }

    public Employee(string employeeId, string lastName, string firstName, string title, string courtesy, string birthdate,
        string hireDate, string address, string city, string region, string postalCode, string country,
        string homePhone, string extension, string photo, string notes, string reportsTo, string photoPath)
    {
        EmployeeId = employeeId;
        LastName = lastName;
        FirstName = firstName;
        Title = title;
        Courtesy = courtesy;
        Birthdate = birthdate;
        HireDate = hireDate;
        Address = address;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Country = country;
        HomePhone = homePhone;
        Extension = extension;
        Photo = photo;
        Notes = notes;
        ReportsTo = reportsTo;
        PhotoPath = photoPath;
    }
}

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