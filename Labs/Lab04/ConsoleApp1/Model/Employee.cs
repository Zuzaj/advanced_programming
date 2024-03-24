namespace ConsoleApp1.Model;

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