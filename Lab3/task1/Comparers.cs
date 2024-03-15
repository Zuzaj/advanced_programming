using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task1
{

    public class CompareByName : IComparer<Tweet>
    {
        public int Compare(Tweet t1, Tweet t2)
        {
            return t1.UserName.CompareTo(t2.UserName);
        }
    }

    public class CompareByDate : IComparer<Tweet>
    {
        public int Compare(Tweet t1, Tweet t2)
        {
            DateTime convertedDate1 = DateTime.Parse(t1.CreatedAt.Replace("at",""));
            DateTime convertedDate2 = DateTime.Parse(t2.CreatedAt.Replace("at",""));
            return convertedDate1.CompareTo(convertedDate2);
        }
    }
}