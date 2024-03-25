using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp1;

public static class PathUtils
{
    public static string GetProjectRoot()
    {
        // this works only in console apps.
        // AppDomain.CurrentDomain.BaseDirectory will return the directory where the .dll / .exe are located
        // usually when u deploy something with IIS / Azure / Docker ect. this will not work.
        // should work for both "dotnet run" and "IDE run" scenarios

        var directory = AppDomain.CurrentDomain.BaseDirectory;
        var projectDirectory = Directory.GetParent(directory)?.Parent?.Parent?.Parent?.FullName ?? throw new InvalidOperationException();
        return projectDirectory;
    }

    public static string SetAsCurrentDirectory(this string path)
    {
        Directory.SetCurrentDirectory(path);
        return path;
    }
    
    public static IEnumerable<T> ReadCsvAsIEnumerable<T>(this string path)
    {
        var config = CsvConfiguration.FromAttributes(typeof(T), CultureInfo.InvariantCulture);
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<T>().ToList();
    }
}