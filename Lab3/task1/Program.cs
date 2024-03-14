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


static void Main(string[] args)
    {

String jsonString = System.IO.File.ReadAllText("favorite-tweets.json");        
Tweet ?tweet = JsonSerializer.Deserialize<Tweet>(jsonString);
Console.WriteLine(tweet.ToString());


    }

}