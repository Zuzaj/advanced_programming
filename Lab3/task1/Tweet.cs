
namespace task1
{
    public class Tweet
    {
           public string ?text { get; set;}
           public string ?author {get; set;}
    public override string ToString()
    {
        return $"Author: {author}, Text: {text}";;
    }
    }

    public class Tweets
{
    public List<Tweet> data { get; set; }
} 
}