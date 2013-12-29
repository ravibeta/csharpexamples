using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatsuoKeywordExtractor
{
    public class Clusterer
    {
        public Dictionary<string, int> WordCount { get; set; }
        public List<KeyValuePair<string, int>> FrequentTerms { get; set; }
        public List<KeyValuePair<string, int>> TopTerms { get; set; }
        public List<Cluster> Clusters {get; private set;}
        public string[] Sentences { get; set; }
        public int[,] CooccurenceMatrix { get; set; }
        public string[] StopWords { get; set; }


        public void Initialize()
        {
            CooccurenceMatrix = PopulateCooccurrenceMatrix(WordCount, Sentences);
        }

        public void Classify()
        {
            int k = 0;
            Clusters = new List<Cluster>();
            var wordCluster = new Dictionary<string, int>(); 
            for (int i = 0; i < FrequentTerms.Count; i++)
                for (int j = 0; j < FrequentTerms.Count; j++)
                {

                    // TODO: we could optimize to using only the diagonal half the matrix [i,j]
                    if (i != j)
                    {
                        var X = FrequentTerms[i].Key;
                        var Y = FrequentTerms[j].Key;
                        int XIndex = wordCluster.Keys.Contains(X) ?  wordCluster[X] : -1;
                        int YIndex = wordCluster.Keys.Contains(Y) ? wordCluster[Y] : -1;
                        if (XIndex == YIndex && XIndex != -1) continue;
                        var measure = GetMutualInformation(X, Y);
                        if (measure > Math.Log(2.0d))
                        {
                            if (XIndex != -1)
                            {
                                if (YIndex == -1)
                                {
                                    wordCluster.Add(Y, XIndex);
                                }
                                Clusters[XIndex].Members.Add(Y);
                            }
                            if (YIndex != -1)
                            {
                                if (XIndex == -1)
                                {
                                    wordCluster.Add(X, YIndex);
                                }
                                Clusters[YIndex].Members.Add(X);
                            }
                            if (XIndex == -1 && YIndex == -1)
                            {
                                Clusters.Add(new Cluster() { Members = new List<string> { X, Y } });
                                wordCluster.Add(X, k);
                                wordCluster.Add(Y, k);
                                k++;
                            }
                            if(XIndex != -1 && YIndex != -1 && Xindex != YIndex)
{
Clusters[XIndex].Members.Merge(Clusters[YIndex].Members);
Clusters[YIndex].Members.ForEach(x => {wordCluster[x] = XIndex;});
Clusters.RemoveAt(YIndex);
}
                    }                    
                }
        }

        internal double GetMutualInformation(string X, string Y)
        {
            var result = 0.0d;
            if (FrequentTerms.Any(x => x.Key == X) == false ||
                FrequentTerms.Any(x => x.Key == Y) == false)
                return result;

            var countX = Sentences.Count(x => x.Contains(X)) / FrequentTerms.Sum(x => x.Value);
            var countY = Sentences.Count(y => y.Contains(Y)) / FrequentTerms.Sum(x => x.Value);
            result = (CooccurenceMatrix[IndexOf(X), IndexOf(Y)] / FrequentTerms.Sum(x => x.Value)) / (countX * countY);
            return result;
        }

        internal int[,] PopulateCooccurrenceMatrix(Dictionary<string, int> dict, string[] sentences)
        {
            var topTerms = dict.OrderByDescending(x => x.Value).ToList();
            TopTerms = topTerms;
            InitializeFrequentTerms(dict);
            var matrix = new int[topTerms.Count(), FrequentTerms.Count()];
            int i = -1;
            foreach (var word in topTerms)
            {
                i++;
                int j = -1;
                foreach (var pair in FrequentTerms)
                {
                    j++;
                    if (word.Key != pair.Key)
                    {
                        if ( i < FrequentTerms.Count() && matrix[j, i] != 0)
                        {
                            matrix[i, j] = matrix[j, i];
                            continue;
                        }
                        var count = sentences.Count(x => x.Contains(word.Key) && x.Contains(pair.Key));
                        matrix[i, j] = count;
                    }
                }
            }

            return matrix;

        }
        
        internal int IndexOf(string term)
        {
            int index = -1;
            var kvp  = WordCount.FirstOrDefault(x => x.Key == term);
            index = TopTerms.IndexOf(kvp);
            return index;
        }

        internal double GetChiSquare(string word)
        {
            int nw = 0;
            double chi = 0;
            var stopWords = File.ReadAllText(@"StopWords.txt").Split(new char[] { ',' });
            StopWords = stopWords;
            Sentences.ToList().ForEach(x => { if (x.Contains(word)) { var words = x.Split(); nw += words.Count(t => StopWords.Contains(t) == false); } });
            foreach ( var g in FrequentTerms)
            {
                if (word != g.Key)
                {
                    double pg = 0;
                    Sentences.ToList().ForEach(x => { if (x.Contains(g.Key)) { var words = x.Split(); pg += words.Count(t => StopWords.Contains(t) == false); } });
                    pg = pg / FrequentTerms.Sum(x => x.Value);
                    var freq_w_g = CooccurenceMatrix[IndexOf(word), IndexOf(g.Key)];
                    double component = ((freq_w_g - nw * pg) * (freq_w_g - nw * pg)) / (nw * pg);
                    chi = chi + component;
                }
            }
            return chi;
        }

        internal void InitializeFrequentTerms(Dictionary<string, int> dict)
        {
            var topTerms = dict.OrderByDescending(x => x.Value);
            var cutoff = topTerms.Sum(x => x.Value) * 0.3d;
            var sum = 0.0d;
            int entries = 0;
            var frequentTerms = topTerms.TakeWhile(x => { entries++; sum += x.Value; return sum < cutoff || entries == 1; }).ToList();
            FrequentTerms = frequentTerms;
        }
    }

    public class Cluster
    {
        public double Center { get; set; }
        public List<string> Members { get; set; }
    }
}
