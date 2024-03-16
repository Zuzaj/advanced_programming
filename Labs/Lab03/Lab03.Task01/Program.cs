using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using task1;

class MyProgram
{
    static Tweets LoadTweetsFromJson(string filePath)
    {
        String jsonString = File.ReadAllText(filePath);
        Tweets? tweets = JsonSerializer.Deserialize<Tweets>(jsonString);
        // foreach (Tweet t in tweets.data)
        // Console.WriteLine(t.ToString());
        return tweets;
    }

    static void ConvertToXml(Tweets tweets, string filePath)
    {
        XmlSerializer x = new XmlSerializer(tweets.GetType());
        using (StreamWriter writer = File.CreateText(filePath))
        {
            x.Serialize(writer, tweets);
        }
    }

    static Dictionary<string, List<Tweet>> LoadToDict(Tweets tweets)
    {
        Dictionary<string, List<Tweet>> d = new Dictionary<string, List<Tweet>>();
        for (int a = 0; a < tweets.data.Count; a++)
        {
            if (d.ContainsKey(tweets.data[a].UserName))
            {
                d[tweets.data[a].UserName].Add(tweets.data[a]);
            }
            else
            {
                List<Tweet> new_list = new List<Tweet>();
                new_list.Add(tweets.data[a]);
                d.Add(tweets.data[a].UserName, new_list);
            }
        }

        return d;
    }

    static Dictionary<string, int> CountWords(Tweets tweets)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
        for (int a = 0; a < tweets.data.Count; a++)
        {
            string[] words = Regex.Split(tweets.data[a].Text, @"\W+");
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    string formattedWord = word.ToLower();
                    if (wordFrequency.ContainsKey(formattedWord))
                    {
                        wordFrequency[formattedWord]++;
                    }
                    else
                    {
                        wordFrequency[formattedWord] = 1;
                    }
                }
            }
        }

        return wordFrequency;
    }

    static List<string> selectFrequent(Tweets tweets)
    {
        Dictionary<string, int> d = CountWords(tweets);
        foreach (KeyValuePair<string, int> entry in d)
        {
            if (entry.Key.Length < 5)
            {
                d.Remove(entry.Key);
            }
        }

        //SortedDictionary<string, int> sd = new SortedDictionary<string, int>();
        // foreach(KeyValuePair<string,int> entry in d){
        //     sd.Add(entry.Key, entry.Value);
        // }
        var sd = d.OrderByDescending(x => x.Value);
        int i = 0;
        List<string> l = new List<string>();
        foreach (KeyValuePair<string, int> entry in sd)
        {
            Console.Write($"Word: {entry.Key} count: {entry.Value},");
            l.Add(entry.Key);
            i++;
            if (i >= 9)
            {
                break;
            }
        }

        return l;
    }

    static Dictionary<string, double> IDF(Tweets tweets)
    {
        double totalNum = tweets.data.Count;
        Dictionary<string, double> countIDF = new Dictionary<string, double>();
        foreach (var tweet in tweets.data)
        {
            string[] words = Regex.Split(tweet.Text, @"\W+");
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    string formattedWord = word.ToLower();
                    if (countIDF.ContainsKey(formattedWord))
                    {
                        countIDF[formattedWord]++;
                    }
                    else
                    {
                        countIDF[formattedWord] = 1;
                    }
                }
            }
        }

        foreach (KeyValuePair<string, double> entry in countIDF)
        {
            countIDF[entry.Key] = Math.Log(totalNum / entry.Value);
        }

        return countIDF;
    }


    static void Main(string[] args)
    {
        Tweets tweets = LoadTweetsFromJson("data.json");

        //  Sortowanie tweetów po nazwie użytkownika
        //   tweets.data.Sort();
        tweets.SortByNick();
        tweets.SortByDate();
        // Console.WriteLine("\nPosortowane tweety po nazwie użytkownika:");
        // foreach (var tweet in tweets.data)
        // {
        //     Console.WriteLine(tweet);
        // }

        //   tweets.SortByDate();
        //  tweets.SortByNick();
        Tweet newest = tweets.FindNewest();
        Tweet oldest = tweets.FindOldest();
        Console.WriteLine($"newest: {newest}, date: {newest.CreatedAt}");
        Console.WriteLine($"oldest: {oldest}, date: {oldest.CreatedAt}");

        // Dictionary<string, List<Tweet>> dict = LoadToDict(tweets);

        // Dictionary<string, int>wordsCounted = CountWords(tweets);

        // foreach(KeyValuePair<string, int> entry in wordsCounted){
        //     Console.WriteLine($"Word: {entry.Key}, count: {entry.Value}");
        // }
        // selectFrequent(tweets);

        // Dictionary<string, double> idfValues = IDF(tweets);

        // var sortedIDF = idfValues.OrderByDescending(x => x.Value).Take(10);
        // foreach(KeyValuePair<string, double>entry in sortedIDF){
        //     Console.WriteLine($"word: {entry.Key}, IDF: {entry.Value}");
        // }
    }
}