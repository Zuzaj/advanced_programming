namespace ConsoleApp1.Model;

public record Details(string OrderId, string ProductId, string UnitPrice, string Quantity, string Discount);

public record Employee(string EmployeeId, string LastName, string FirstName, string Title, string Courtesy,
    string Birthdate, string HireDate, string Address, string City, string Region, string PostalCode,
    string Country, string HomePhone, string Extension, string Photo, string Notes, string ReportsTo, string PhotoPath);

public record EmployeeTerritory(string EmployeeId, string TerritoryId);

public record Order(string OrderId, string CustomerId, string EmployeeId, string OrderDate, string RequiredDate,
    string ShippedDate, string ShipVisa, string Freight, string ShipName, string ShipAddress,
    string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry);

public record Region(string RegionId, string RegionDescription);

public record Territory(string TerritoryId, string TerritoryDescription, string RegionId);
