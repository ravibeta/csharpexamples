using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkmDataStructures2;
using LAIR.ResourceAPIs.WordNet;
using System.IO;

// This is a placeholder for a windows phone application
namespace BookIndexing
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Clustering and Segmentation. Clustering is a data mining technique that is directed towards the goals of identification and classification. Clustering tries to identify a finite set of categories or clusters to which each data object (tuple) can be mapped. The categories may be disjoint or overlapping and may sometimes be organized into trees. For example, one might form categories of customers into the form of a tree and then map each customer to one or more of the categories. A closely related problem is that of estimating multivariate probability density functions of all variables that could be attributes in a relation or from different relations.";
            var skipList = Parse(text);
            var indexer = new SalientWords(skipList);
            var ret = indexer.Index();
            Console.WriteLine("Index");
            foreach( var r in ret)
                Console.WriteLine(r.Key + " " + r.Value);
        }

        public static SkipList<WordInfo> Parse(string text, int pageSize = 0)
        {
            var skipList = new SkipList<WordInfo>();
            var words = text.Split(new char[] { ' ', '\t', '\n' });
            int index = 0;
            foreach (var word in words)
            {
                var canon = word.Trim(new char[] { '.', ',', ';', ':', '-' });
                index = text.IndexOf(word, index);
                int size = (pageSize == 0 ? text.Length : pageSize);

                // lookup existing skiplist and update offset
                var node  = skipList.Find(new WordInfo() { Word = word });
                if (node != null)
                {
                    node.Value.Frequency++;
                    int i = 0;
                    while (node.Value.Offset[i] != 0) i++;
                    if (i < 32)
                    {
                        node.Value.Offset[i] = index;
                        node.Value.Page[i] = index / size + 1;
                    }
                }
                else
                {
                    var wordInfo = new WordInfo() { Word = word, Canon = canon, Frequency = 1, Offset = new Int64[32], Page = new Int32[32] };
                    wordInfo.Offset[0] = index;
                    wordInfo.Page[0] = index / size + 1;
                    skipList.Add(wordInfo);                    
                }
            }
            return skipList;
        }



        public abstract class IndexGenerator
        {
            public SkipList<WordInfo> Candidates { get; set; }
            public abstract Dictionary<string, string> Index(); 
        }

        public class Context<T>
        {
            public Context(T value)
            {
                Classifier = value;
            }
            public Dictionary<string, string> GenerateIndex() 
            {
                return (Classifier as IndexGenerator).Index(); 
            }
            public T Classifier { get; set; }
        }

        public class SalientWords : IndexGenerator
        {
            public SalientWords(SkipList<WordInfo> skipList)
            {
                base.Candidates = skipList;
            }
            public override Dictionary<string, string> Index()
            {
                var ret = new Dictionary<string, string>();
                if (Candidates.Count != 0)
                {
                    var filter = new List<string> { "a", "an", "all", "and", "be", "can", "could", "each", "For", "in", "into", "is", "of", "or", "the", "which" };
                    var selected = Candidates.Where(x => filter.Contains(x.Word) == false);
                    selected = selected.Where(x => x.Word.Length > 5 && x.Frequency > 2);
                    var relationFactor = new Dictionary<string, int>();
                    foreach (var word in selected)
                    {
                        var fi = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        var engine = new WordNetEngine(fi.DirectoryName, true);
                        var allwords = engine.AllWords;
                        foreach (var other in selected)
                        {
                            if (word != other)
                            {
                                var synset1 = engine.GetSynSets(word.Word,  null);
                                var synset2 = engine.GetSynSets(other.Word, null);
                                if (synset1 != null && synset1.Count > 0 
                                    && synset2 != null && synset2.Count > 0)
                                {
                                    var spt = synset1.ElementAt(0).GetShortestPathTo(synset2.ElementAt(0), null);
                                    if (spt != null)
                                        if (relationFactor.Keys.Contains(word.Word))
                                            relationFactor[word.Word] += spt.Count;
                                        else
                                            relationFactor.Add(word.Word, spt.Count);
                                }
                                else
                                {
                                    if (relationFactor.Keys.Contains(word.Word))
                                        relationFactor[word.Word] += 0;
                                    else
                                        relationFactor.Add(word.Word, 0);
                                }
                            }
                        }
                    }
                    int mostRelated = 0;
                    if (relationFactor.Count() > 0)
                        mostRelated = relationFactor.Max(x => x.Value/relationFactor.Count);
                    var related = new List<String>();
                    foreach (var kvp in relationFactor)
                        if(kvp.Value >= mostRelated)
                            related.Add(kvp.Key);
                    //var discrete = new List<string>();
                    //for (int k = 0; k < mostRelated + 1; k++)
                    //{
                    //    string word = string.Empty;
                    //    var cluster = new List<string>();
                    //    foreach(var kvp in relationFactor)
                    //        if(kvp.Value >= k && kvp.Value < k + 1)
                    //            cluster.Add(kvp.Key);
                    //    var clusterset = Candidates.Where(x =>cluster.Contains(x.Word));
                    //    if (clusterset != null)
                    //    {
                    //        var max = clusterset.Max(x => x.Frequency);
                    //        discrete.Add(clusterset.Single(x=>x.Frequency == max).Word);
                    //    }
                    //}

                    selected = selected.Where(x => related.Contains(x.Word));
                    int i = 0;
                    foreach (var t in selected)
                    {
                        if (i < 2)
                        {
                            string pages = string.Empty;
                            foreach (var page in t.Page)
                            {
                                if (page != 0)
                                {
                                    var sofar = pages.Split(new char[] { ',' });
                                    if (sofar.Length > 1)
                                    {
                                        var last = sofar[sofar.Length - 2];
                                        if (last == page.ToString())
                                            continue;
                                    }
                                    pages += page.ToString() + ',';
                                }
                            }
                            pages.TrimEnd(new char[] { ',' });
                            ret.Add(t.Canon.ToLower(), pages);
                            i++;
                        }
                    }
                }
                return ret;
            }
        }
    }
}
