using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clusterer;
using System.Collections.Generic;
using System.Linq;

namespace ClustererTest
{
    [TestClass]
    public class ClustererTest
    {
        [TestMethod]
        public void DocumentTest()
        {
            var document = new Document();
            document.Vocabulary = new List<string>() { "apple", "banana", "orange" };
            document.FreqDistributionOfTermsFromVocabulary = new Dictionary<string, int>() { };
            document.FreqDistributionOfTermsFromVocabulary["apple"] = 2;
            document.FreqDistributionOfTermsFromVocabulary["banana"] = 3;
            document.Probabilities = new List<decimal>();
            foreach (var term in document.Vocabulary)
            {
                document.Probabilities.Add(document.GetProbability(term));
            }
            var total = document.Probabilities.Sum();
            Assert.IsTrue(total == Convert.ToDecimal(1.0));
        }
    }
}
