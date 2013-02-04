using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// This is a placeholder for a windows phone application
namespace BookIndexing
{
    public struct wordInfo
    {
        int offset;
        int count;
    };

    class Program
    {
        static void Main(string[] args)
        {
            string text = "Clustering and Segmentation. Clustering is a data mining technique that is directed towards the goals of identification and classification. Clustering tries to identify a finite set of categories or clusters to which each data object (tuple) can be mapped. The categories may be disjoint or overlapping and may sometimes be organized into trees. For example, one might form categories of customers into the form of a tree and then map each customer to one or more of the categories. A closely related problem is that of estimating multivariate probability density functions of all variables that could be attributes in a relation or from different relations.";
            var words = text.Split(new char[] { ' ', '\t', '\n' });
            var sdict = new SortedDictionary<string, wordInfo>();
            foreach (var word in words)
            {
                var canon = word.Trim();
            }
        }
    }
}
