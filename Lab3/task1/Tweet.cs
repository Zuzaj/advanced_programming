
namespace task1
{
    public class Tweet
    {
           public string ?text { get; set;}
           public string ?user_nick {get; set;}

           public string ?date {get; set;}
            public override string ToString()
            {
                return $"Author: {user_nick}, Text: {text}";;
            }

            public bool Includes(string myWord){
                string[] words = System.Text.RegularExpressions.Regex.Split(text, @"\W+");
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
        return this.data[data.Count-1];
    }

    public Tweet FindNewest()
    {
        this.SortByDate();
        return this.data[0];
    }

} 
    }

