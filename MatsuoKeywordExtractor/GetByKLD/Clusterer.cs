using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByKLD
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
        internal double ThresholdFactor { get; set; }
        public int KMeans { get; set; }
        public const double EPSILON = 0.10d;
        public List<KLDLabel> Labels { get; set; }


        public void Initialize(Dictionary<string,int> dict, string[] sentences)
        {
            if (ThresholdFactor == 0.0d) ThresholdFactor = 0.3d;
            var topTerms = dict.OrderByDescending(x => x.Value).ToList();
            Sentences = sentences.ToList().ConvertAll(x => x.ToLower()).ToArray();
            TopTerms = topTerms;
            WordCount = dict;
            InitializeFrequentTerms(dict);
            CooccurenceMatrix = PopulateCooccurrenceMatrix();
            if (KMeans == 0) KMeans = 3;
        }

        public void Classify()
        {
            int k = 0;
            Clusters = new List<Cluster>();
            var wordCluster = new Dictionary<string, int>();

            for (int i = 0; i < FrequentTerms.Count; i++)
                for (int j = 0; j < FrequentTerms.Count; j++)
                {
                    var X = FrequentTerms[i].Key;
                    var Y = FrequentTerms[j].Key;
                    int XIndex = wordCluster.Keys.Contains(X) ? wordCluster[X] : -1;
                    int YIndex = wordCluster.Keys.Contains(Y) ? wordCluster[Y] : -1;
                    if (XIndex == YIndex && XIndex != -1) continue;

                    // TODO: we could optimize to using only the diagonal half the matrix [i,j]
                    if (i != j)
                    {
                        var measure = GetMutualInformation(X, Y);
                        if (measure > Math.Log(2.0d))
                        {
                            if (XIndex != -1 && YIndex == -1)
                            {
                                wordCluster.Add(Y, XIndex);
                                k++;
                                Clusters[XIndex].Members.Add(Y);
                            }
                            if (YIndex != -1 && XIndex == -1)
                            {
                                wordCluster.Add(X, YIndex);
                                k++;
                                Clusters[YIndex].Members.Add(X);
                            }
                            if (XIndex == -1 && YIndex == -1)
                            {
                                Clusters.Add(new Cluster() { Members = new List<string> { X, Y } });
                                wordCluster.Add(X, k);
                                wordCluster.Add(Y, k);
                                k++;
                            }
                            if (XIndex != -1 && YIndex != -1 && XIndex != YIndex)
                            {
                                if (XIndex < YIndex)
                                {
                                    Clusters[XIndex].Members.AddRange(Clusters[YIndex].Members);
                                    Clusters[YIndex].Members.ForEach(x => { wordCluster[x] = XIndex; });
                                    Clusters.RemoveAt(YIndex);
                                }
                                else
                                {
                                    Clusters[YIndex].Members.AddRange(Clusters[XIndex].Members);
                                    Clusters[XIndex].Members.ForEach(x => { wordCluster[x] = YIndex; });
                                    Clusters.RemoveAt(XIndex);
                                }
                            }
                        }
                    }
                    if (wordCluster.Keys.Contains(X) == false)
                    {
                        wordCluster.Add(X, k);
                        k++;
                        Clusters.Add(new Cluster() { Members = new List<string>() { X } });
                    }
                }
        }

        internal List<string> GetKeywordsBasedOnKLD()
        {
            var ret = new List<String>();
            double epsilon = 0.1d;
            var sum = TopTerms.Sum(x => x.Value);
            if (sum > 0)
            {
                foreach (var kvp in TopTerms)
                {
                    double kld = (1.0d * Math.Log(1 / (((double)kvp.Value) / sum)));  // when will be less than 1.0 ? use epsilon for other terms
                    foreach (var term in TopTerms)
                    {
                        if (term.Key != kvp.Key)
                        {
                            kld += (epsilon * Math.Log(epsilon / (((double)term.Value) / sum)));
                        }
                    }
                    if (kld > 1.0d)
                    {
                        ret.Add(kvp.Key);
                    }
                }
            }
            return ret;
        }

        internal double GetMutualInformation(string X, string Y)
        {
            var result = 0.0d;
            if (FrequentTerms.Any(x => x.Key == X) == false ||
                FrequentTerms.Any(x => x.Key == Y) == false)
                return result;

            double countX = ((double)Sentences.Count(x => x.Contains(X))) / FrequentTerms.Sum(x => x.Value);
            double countY = ((double)Sentences.Count(y => y.Contains(Y))) / FrequentTerms.Sum(x => x.Value);
            int indexX = IndexOf(X);
            int indexY = IndexOf(Y);
            if (indexX < CooccurenceMatrix.Length && indexY < CooccurenceMatrix.Rank)
                result = ((double)CooccurenceMatrix[IndexOf(X), IndexOf(Y)] / FrequentTerms.Sum(x => x.Value)) / (countX * countY);
            return result;
        }

        internal int[,] PopulateCooccurrenceMatrix()
        {
            var matrix = new int[TopTerms.Count(), FrequentTerms.Count()];
            int i = -1;
            foreach (var word in TopTerms)
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
                        var count = Sentences.Count(x => x.Contains(word.Key) && x.Contains(pair.Key));
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
            var cutoff = topTerms.Sum(x => x.Value) * ThresholdFactor;
            var sum = 0.0d;
            int entries = 0;
            FrequentTerms = topTerms.TakeWhile(x => { entries++; if (sum < cutoff || entries == 1) { sum += x.Value; return true; } else { return false; } }).ToList();
        }

        //  http://ravinote.blogspot.com/2014/01/both-zipfs-law-of-word-occurrences-and.html
        internal void CoOccurrenceClassify()
        {
            // Initialize centroids
            Clusters = InitializeKMeansCluster();

            // Repeat
            List<string> prev = null;
            List<string> current = new List<string>();
            do
            {
                AssignLabel();

                for (int i = 0; i < KMeans; i++)
                {
                    FixCentroids(i);
                }
                prev = current;
                current = Clusters.Select(x => x.Term).ToList();
            }
            while (prev == null || current.SequenceEqual(prev) == false);

        }

        private List<Cluster> InitializeKMeansCluster()
        {
            var clusters = new List<Cluster>();
            for (int i = 0; i < KMeans; i++)
            {
                int count = clusters.Count;
                while (clusters.Count == count)
                {
                    var rand = new Random();
                    int n = rand.Next(0, TopTerms.Count - 1);
                    var term = TopTerms[n].Key;
                    if (clusters.Any(x => x.Term == term)) continue;
                    clusters.Add(new Cluster() { Term = term });
                }
            }
            Labels = new List<KLDLabel>();
            TopTerms.ForEach(t => Labels.Add(new KLDLabel() { Term = t.Key, ClusterIndex = 0 }));
            return clusters;
        }

        private void AssignLabel()
        {
            Labels.ForEach(x =>
            {
                var distances = new List<double>();
                if (Clusters.Any(t => t.Term == x.Term) == false)
                {
                    for (int i = 0; i < KMeans; i++)
                    {
                        distances.Add(GetKLDDistance(Clusters[i].Term, x.Term));
                    }
                    var min = distances.Min();
                    int index = distances.IndexOf(min);

                    x.ClusterIndex = index;
                }
                else
                {
                    x.ClusterIndex = Clusters.IndexOf(Clusters.FirstOrDefault(r => r.Term == x.Term));
                }
            });
        }

        private void FixCentroids(int  n)
        {
            var members = Labels.Where(x => x.ClusterIndex == n).ToList();
            List<int> rowTotals = new List<int>();
            members.ForEach(x =>
                {
                    var keys = TopTerms.Select(t => t.Key).ToList();
                    int ix = keys.IndexOf(x.Term);
                    int sum = 0;
                    for (int i = 0; i < CooccurenceMatrix.Rank; i++)
                    {
                        sum += CooccurenceMatrix[ix, i];
                    }
                    rowTotals.Add(sum);
                });
            if (members.Count > 0)
            {
                var avg = rowTotals.Average();
                var sorted = rowTotals;
                sorted.Sort();
                int candidate = 0;
                while (candidate < sorted.Count && sorted[candidate] <= avg) candidate++; // use binary search
                if (candidate >= sorted.Count) candidate = sorted.Count - 1;
                int index = rowTotals.IndexOf(sorted[candidate]);
                Clusters[n].Term = members[index].Term;
            }
        }

        private double GetKLDDistance(string x, string y)
        {
            double distance = 0d;
            var keys = TopTerms.Select(t => t.Key).ToList();
            int ix = keys.IndexOf(x);
            int iy = keys.IndexOf(y);
            for (int i = 0; i < CooccurenceMatrix.Rank; i++)
            {
                if (CooccurenceMatrix[iy, i] == 0)
                    distance += EPSILON;
                else
                    distance += CooccurenceMatrix[ix, i] * Math.Log(CooccurenceMatrix[ix, i] / CooccurenceMatrix[iy, i]);
            }
            return distance;
        }
    }

    public class Cluster
    {
        public double Center { get; set; }
        public string Term { get; set; }
        public List<string> Members { get; set; }
    }

    public class KLDLabel
    {
        public string Term { get; set; }
        public int ClusterIndex { get; set; }
    }

}
