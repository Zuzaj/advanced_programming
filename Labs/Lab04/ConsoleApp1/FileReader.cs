namespace ConsoleApp1;

public class FileReader<T>
{
    public List<T> ToList(string path, Func<string[], T> generate)
    {
        var list = new List<T>();
        using var reader = new StreamReader(path);

        reader.ReadLine();
        while (reader.ReadLine() is { } line)
        {
            var features = line.Split(',');
            list.Add(generate(features));
        }

        return list;
    }
}