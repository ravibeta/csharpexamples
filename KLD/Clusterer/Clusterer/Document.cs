using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterer
{
    public class Document 
    {
        // based on Kullback-Leibler 
        public List<double> Probabilities { get; set; }

        public const double EPSILONPROBABILITY = 0.01d;

        public List<string> Vocabulary { get; set; }
        
        public Dictionary<string, int> FreqDistributionOfTermsFromVocabulary { get; set; }

        public Label Label { get; set; }

        public double GetProbability(string term)
        {
            double probability = EPSILONPROBABILITY;
            if (FreqDistributionOfTermsFromVocabulary.Keys.Contains(term) == false)
            {
                return probability;
            }

            int total = 0;
            foreach (var kvp in FreqDistributionOfTermsFromVocabulary)
            {
                total += kvp.Value;
            }

            if (total != 0)
            {
                probability = Convert.ToDouble(FreqDistributionOfTermsFromVocabulary[term]) / Convert.ToDouble(total);
                probability = GetScalingFactor() * Convert.ToDouble(probability);
            }

            return probability; 
        }

        public double GetScalingFactor()
        {
            var numUnknownTerms = Vocabulary.Count() - FreqDistributionOfTermsFromVocabulary.Count();
            Debug.Assert(numUnknownTerms > 0);
            return 1 - numUnknownTerms * EPSILONPROBABILITY;
        }

        public static Document GetEmptyDocument(List<string> vocabulary)
        {
            Document d = new Document();
            d.FreqDistributionOfTermsFromVocabulary = new Dictionary<string, int>();
            d.Vocabulary = vocabulary;
            d.Probabilities = new List<double>();
            foreach (var v in vocabulary)
            {
                d.Probabilities.Add(EPSILONPROBABILITY);
            }
            return d;
        }
    }
}
