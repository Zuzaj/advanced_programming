
namespace task1
{
    public class Tweet
    {
           public string ?Text { get; set;}
           public string ?UserName {get; set;}
           public string ?LinkToTweet {get; set;}
           public string ?FirstLinkUrl {get; set;}
           public string ?CreatedAt {get; set;}
           public string ?TweetEmbedCode {get; set;}

            public override string ToString()
            {
                return $"Author: {UserName}, Text: {Text}";;
            }

            public bool Includes(string myWord){
                string[] words = System.Text.RegularExpressions.Regex.Split(Text, @"\W+");
                foreach( string word in words){
                if(!string.IsNullOrWhiteSpace(word))
            {
                string formattedWord = word.ToLower();
            
            if ( myWord == formattedWord){
                return true;
                }
            }
            }
            return false;
            }

            // public int CompareTo(Tweet? other){
            //     return this.Text.CompareTo(other?.Text);
            // }
    }


    public class Tweets
{
    public List<Tweet> data { get; set; }

    public void SortByNick()
    {
        CompareByName comp = new CompareByName();
        data.Sort(comp);

    }

    public void SortByDate()
    {
       CompareByDate comp = new CompareByDate();
       data.Sort(comp); 
    }

    public Tweet FindOldest()
    {
        this.SortByDate();
        return this.data[0];
    }

    public Tweet FindNewest()
    {
        this.SortByDate();
        return this.data[data.Count-1];
    }

} 
    }

