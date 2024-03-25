using CsvHelper.Configuration.Attributes;

namespace ConsoleApp1.Model;

public class Employee
{
    [Name("employeeid")] public string EmployeeId { get; init; } = string.Empty;
    [Name("lastname")] public string LastName { get; init; } = string.Empty;
    [Name("firstname")] public string FirstName { get; init; } = string.Empty;
    [Name("title")] public string Title { get; init; } = string.Empty;
    [Name("titleofcourtesy")] public string Courtesy { get; init; } = string.Empty;
    [Name("birthdate")] public string Birthdate { get; init; } = string.Empty;
    [Name("hiredate")] public string HireDate { get; init; } = string.Empty;
    [Name("address")] public string Address { get; init; } = string.Empty;
    [Name("city")] public string City { get; init; } = string.Empty;
    [Name("region")] public string Region { get; init; } = string.Empty;
    [Name("postalcode")] public string PostalCode { get; init; } = string.Empty;
    [Name("country")] public string Country { get; init; } = string.Empty;
    [Name("homephone")] public string HomePhone { get; init; } = string.Empty;
    [Name("extension")] public string Extension { get; init; } = string.Empty;
    [Name("photo")] public string Photo { get; init; } = string.Empty;
    [Name("notes")] public string Notes { get; init; } = string.Empty;
    [Name("reportsto")] public string ReportsTo { get; init; } = string.Empty;
    [Name("photopath")] public string PhotoPath { get; init; } = string.Empty;
}