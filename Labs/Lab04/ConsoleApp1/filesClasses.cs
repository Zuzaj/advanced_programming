namespace ConsoleApp1;

public class Region
{
    public string regionId;
    public string regionDescription;

    public Region(string id, string description)
    {
        regionId = id;
        regionDescription = description;
    }
}

public class Territory
{
    public string territoryId;
    public string territoryDescription;
    public string regionId;

    public Territory(string id_t, string description, string id_r)
    {
        territoryId = id_t;
        territoryDescription = description;
        regionId = id_r;
    }
}

public class Employee
{
    public string employeeId;
    public string lastName;
    public string firstName;
    public string title;
    public string courtesy;
    public string birthdate;
    public string hiredate;
    public string address;
    public string city;
    public string region;
    public string postalCode;
    public string country;
    public string homePhone;
    public string extension;
    public string photo;
    public string notes;
    public string reportsTo;
    public string photoPath;

    public Employee(string employeeId_, string lastName_, string firstName_, string title_, string courtesy_, string birthdate_,
        string hiredate_, string address_, string city_, string region_, string postalCode_, string country_,
        string homePhone_, string extension_, string photo_, string notes_, string reportsTo_, string photoPath_)
    {
        employeeId = employeeId_;
        lastName = lastName_;
        firstName = firstName_;
        title = title_;
        courtesy = courtesy_;
        birthdate = birthdate_;
        hiredate = hiredate_;
        address = address_;
        city = city_;
        region = region_;
        postalCode = postalCode_;
        country = country_;
        homePhone = homePhone_;
        extension = extension_;
        photo = photo_;
        notes = notes_;
        reportsTo = reportsTo_;
        photoPath = photoPath_;
    }
}

public class EmployeeTerritory
{
    public string employeeId;
    public string territoryId;

    public EmployeeTerritory(string employeeId_, string territoryId_)
    {
        employeeId = employeeId_;
        territoryId = territoryId_;
    }
}

public class Order
{
    public string orderId;
    public string customerId;
    public string employeeId;
    public string orderDate;
    public string requiredDate;
    public string shippedDate;
    public string shipVisa;
    public string freight;
    public string shipName;
    public string shipAddress;
    public string shipCity;
    public string shipRegion;
    public string shipPostalCode;
    public string shipCountry;

    public Order(string orderId_, string customerId_, string employeeId_, string orderDate_,
        string requiredDate_, string shippedDate_, string shipVisa_, string freight_, string shipName_,
        string shipAddress_, string shipCity_, string shipRegion_, string shipPostalCode_, string shipCountry_)
    {
        orderId = orderId_;
        customerId = customerId_;
        employeeId = employeeId_;
        orderDate = orderDate_;
        requiredDate = requiredDate_;
        shippedDate = shippedDate_;
        shipVisa = shipVisa_;
        freight = freight_;
        shipName = shipName_;
        shipAddress = shipAddress_;
        shipCity = shipCity_;
        shipRegion = shipRegion_;
        shipPostalCode = shipPostalCode_;
        shipCountry = shipCountry_;
    }
}

public class Details
{
    public string orderId;
    public string productId;
    public string unitPrice;
    public string quantity;
    public string discount;

    public Details(string orderId_, string productId_, string unitPrice_, string quantity_, string discount_)
    {
        orderId = orderId_;
        productId = productId_;
        unitPrice = unitPrice_;
        quantity = quantity_;
        discount = discount_;
    }
}