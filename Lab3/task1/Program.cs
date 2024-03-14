using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using task1;


class MyProgram{

static Tweets LoadTweetsFromJson(string filePath)
{
    String jsonString = System.IO.File.ReadAllText(filePath);        
    Tweets ?tweets = JsonSerializer.Deserialize<Tweets>(jsonString);
    foreach (Tweet t in tweets.data)
    Console.WriteLine(t.ToString());
    return tweets;
}

static void ConvertToXml(Tweets tweets, string filePath)
{
    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(tweets.GetType());
    using (StreamWriter writer = File.CreateText(filePath)){
        x.Serialize(writer, tweets);
    }
}

static Dictionary<string, Tweet>LoadToDict(Tweets tweets)
{
    Dictionary<string, Tweet> d = new Dictionary<string, Tweet>();
    for (int a = 0;a < tweets.data.Count; a++){
        d.Add(tweets.data[a].user_nick, tweets.data[a]);
    }
    return d;
}

static Dictionary<string, int>CountWords(Tweets tweets)
{
    Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
    for(int a=0; a< tweets.data.Count; a++)
    {
        string[] words = System.Text.RegularExpressions.Regex.Split(tweets.data[a].text, @"\W+");
        foreach( string word in words)
        {
            if(!string.IsNullOrWhiteSpace(word))
            {
                string formattedWord = word.ToLower();
                if (wordFrequency.ContainsKey(formattedWord)){
                    wordFrequency[formattedWord]++;
                }
                else{
                    wordFrequency[formattedWord] = 1;
                }
            }
        }
    }
    return wordFrequency;
}

static List<string> selectFrequent(Tweets tweets){
    Dictionary<string, int> d = CountWords(tweets);
    foreach(KeyValuePair<string,int> entry in d){
        if(entry.Key.Length < 5){
            d.Remove(entry.Key);
        }
    }
    SortedDictionary<string, int> sd = new SortedDictionary<string, int>();
    foreach(KeyValuePair<string,int> entry in d){
        sd.Add(entry.Key, entry.Value);
    }
    int i =0;
    List<string> l = new List<string>();
    foreach(KeyValuePair<string, int> entry in sd)
    {
        Console.Write(entry.Key + ", ");
        l.Add(entry.Key);
        i++;
        if (i>=9){
            break;
        }
    }
    return l;
    }

    static double IDF(Tweets tweets, string word){
        double totalNum = tweets.data.Count;
        double countIncluding = 0;
        foreach(var tweet in tweets.data){
            if(tweet.Includes(word)){
                countIncluding++;
            }
        }
        return Math.Log(totalNum/countIncluding);
    }








static void Main(string[] args)
    {



            Tweets tweets = LoadTweetsFromJson("favorite-tweets.jsonl");

            // Sortowanie tweetów po nazwie użytkownika
            tweets.data.Sort();
            Console.WriteLine("\nPosortowane tweety po nazwie użytkownika:");
            foreach (var tweet in tweets.data)
            {
                Console.WriteLine(tweet);
            }

            tweets.SortByDate();
            tweets.SortByNick();
            Tweet newest = tweets.FindNewest();
            Tweet oldest = tweets.FindOldest();
            Console.WriteLine($"newest: {newest}");
            Console.WriteLine($"oldest: {oldest}");
            Dictionary<string, int>wordsCounted = CountWords(tweets);
            List<string> frequent = selectFrequent(tweets);

            



    }

}