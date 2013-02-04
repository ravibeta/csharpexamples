using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookIndexing
{
    public class WordInfo
    {
        public string Word { get; set; }
        public string Canon { get; set; }
        public Int64[] Offset { get; set; } // char count
        public Int32[] Page { get; set; }
    }

    public class WordInfoComparer : IComparer<WordInfo>
    {
        public int Compare(WordInfo x, WordInfo y)
        {
            return string.Compare(x.Word, y.Word);
        }
    }

}
