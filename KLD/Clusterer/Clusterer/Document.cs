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
        public List<decimal> Probabilities { get; set; }

        public const decimal EPSILONPROBABILITY = 0.01m;

        public List<string> Vocabulary { get; set; }
        
        public Dictionary<string, int> FreqDistributionOfTermsFromVocabulary { get; set; }


        public decimal GetProbability(string term)
        {
            decimal probability = EPSILONPROBABILITY;
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
                probability = Convert.ToDecimal(FreqDistributionOfTermsFromVocabulary[term]) / Convert.ToDecimal(total);
                probability = GetScalingFactor() * Convert.ToDecimal(probability);
            }

            return probability; 
        }

        public decimal GetScalingFactor()
        {
            var numUnknownTerms = Vocabulary.Count() - FreqDistributionOfTermsFromVocabulary.Count();
            Debug.Assert(numUnknownTerms > 0);
            return 1 - numUnknownTerms * EPSILONPROBABILITY;
        }
    }
}
