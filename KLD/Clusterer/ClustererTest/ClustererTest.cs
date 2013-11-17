using System;
using System.Collections.Generic;
using System.Linq;
using Clusterer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClustererTest
{
    [TestClass]
    public class ClustererTest
    {
        private Document di;

        [TestInitialize]
        public void Initialize()
        {
            var document = new Document();
            document.Vocabulary = new List<string>() { "apple", "banana", "orange" };
            document.FreqDistributionOfTermsFromVocabulary = new Dictionary<string, int>() { };
            document.FreqDistributionOfTermsFromVocabulary["apple"] = 2;
            document.FreqDistributionOfTermsFromVocabulary["banana"] = 3;
            document.Probabilities = new List<double>();
            foreach (var term in document.Vocabulary)
            {
                document.Probabilities.Add(document.GetProbability(term));
            }
            di = document;
        }

        [TestMethod]
        public void DocumentTest()
        {   
            var total = di.Probabilities.Sum();
            Assert.IsTrue(total == Convert.ToDouble(1.0d));
        }

        [TestMethod]
        public void TestKLDDistance()
        {
            var dj = new Document();
            dj.Vocabulary = new List<string>() { "apple", "banana", "orange" };
            dj.FreqDistributionOfTermsFromVocabulary = new Dictionary<string, int>() { };
            dj.FreqDistributionOfTermsFromVocabulary["banana"] = 3;
            dj.FreqDistributionOfTermsFromVocabulary["orange"] = 1;
            dj.Probabilities = new List<double>();
            foreach (var term in dj.Vocabulary)
            {
                dj.Probabilities.Add(dj.GetProbability(term));
            }
            var distance = new Distance();
            var KLD = distance.GetDistance(di, dj);
            Assert.IsTrue(KLD > 0);
        }
    }
}
