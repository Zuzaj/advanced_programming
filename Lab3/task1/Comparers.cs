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
            return t1.user_nick.CompareTo(t2.user_nick);
        }
    }

    public class CompareByDate : IComparer<Tweet>
    {
        public int Compare(Tweet t1, Tweet t2)
        {
            return t1.date.CompareTo(t2.date);
        }
    }
}