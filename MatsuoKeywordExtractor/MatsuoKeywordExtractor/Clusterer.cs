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
        public string[] Sentences { get; set; }
        public int[,] CooccurenceMatrix { get; set; }
        public string[] StopWords { get; set; }


        public void Classify()
        {
            CooccurenceMatrix = PopulateCooccurrenceMatrix(WordCount, Sentences);
        }


        internal int[,] PopulateCooccurrenceMatrix(Dictionary<string, int> dict, string[] sentences)
        {
            var topTerms = dict.OrderByDescending(x => x.Value);
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

        internal double GetChiSquare(string word)
        {
            int nw = 0;
            double chi = 0;
            var stopWords = File.ReadAllText(@"StopWords.txt").Split(new char[] { ',' });
            StopWords = stopWords;
            Sentences.ToList().ForEach(x => { if (x.Contains(word)) { var words = x.Split(); nw += words.Count(t => StopWords.Contains(t) == false); } });
            foreach ( var g in FrequentTerms)
            {
                double pg = 0;
                Sentences.ToList().ForEach(x => { if (x.Contains(g.Key)) { var words = x.Split(); pg += words.Count(t => StopWords.Contains(t) == false); } });
                pg = pg / FrequentTerms.Count();
                double component = ((g.Value - nw * pg) * (g.Value - nw * pg)) / (nw * pg);
                chi = chi + component;
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
}
