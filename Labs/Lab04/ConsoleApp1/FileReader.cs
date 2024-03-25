namespace ConsoleApp1;

public class FileReader<T>
{
    public List<T> ToList(String path, Func<String[], T> generate)
    {
        var list = new List<T>();
        using (var reader = new StreamReader(path))
        {
            string line;
            line = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                var features = line.Split(',');
                list.Add(generate(features));
            }
        }

        return list;
    }
}