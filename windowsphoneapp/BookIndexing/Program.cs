using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkmDataStructures2;

// This is a placeholder for a windows phone application
namespace BookIndexing
{
    class Program
    {
        static void Main(string[] args)
        {
            var skipList = new SkipList<WordInfo>();
            string text = "Clustering and Segmentation. Clustering is a data mining technique that is directed towards the goals of identification and classification. Clustering tries to identify a finite set of categories or clusters to which each data object (tuple) can be mapped. The categories may be disjoint or overlapping and may sometimes be organized into trees. For example, one might form categories of customers into the form of a tree and then map each customer to one or more of the categories. A closely related problem is that of estimating multivariate probability density functions of all variables that could be attributes in a relation or from different relations.";
            var words = text.Split(new char[] { ' ', '\t', '\n' });
            var sdict = new SortedDictionary<string, int>();
            int index = 0;
            foreach (var word in words)
            {
                var canon = word.Trim(new char[] { '.', ',', ';', ':', '-' });
                index = text.IndexOf(word, index);
                if (sdict.ContainsKey(word))
                {
                    sdict[word]++;
                    // lookup existing skiplist and update offset
                }
                else
                {
                    var wordInfo = new WordInfo() { Word = word, Canon = canon, Frequency = 1, Offset = new Int64[32]};
                    wordInfo.Offset[0] = index; 
                    skipList.Add(wordInfo);
                }
            }
        }
    }
}
