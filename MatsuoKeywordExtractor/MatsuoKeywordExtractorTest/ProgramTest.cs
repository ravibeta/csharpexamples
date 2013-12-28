using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatsuoKeywordExtractor;
using System.Collections.Generic;

namespace MatsuoKeywordExtractorTest
{
    [TestClass]
    public class ProgramTest
    {
        private Clusterer clusterer;
        [TestInitialize]
        public void Initialize()
        {
            clusterer = new Clusterer(); 
        }

        [TestMethod]
        public void TestMethod1()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 2);
            dict.Add("ghi", 1);
            var sentences = new string[] { "abc def ghi", "abc def", "abc" };
            var matrix = clusterer.PopulateCooccurrenceMatrix(dict, sentences);
            Assert.IsTrue(matrix[1, 0] == 2);
            Assert.IsTrue(matrix[2, 0] == 1);

        }

        [TestMethod]
        public void TestMethod2()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 2);
            dict.Add("ghi", 1);
            var sentences = new string[] { "abc def ghi", "abc def", "abc" };
            clusterer.Sentences = sentences;
            clusterer.WordCount = dict;
            clusterer.InitializeFrequentTerms(dict);
            clusterer.Initialize();
            var chisquare =  clusterer.GetChiSquare("abc");
            Assert.IsTrue(chisquare == 0);
            var chisquare1 = clusterer.GetChiSquare("def");
            Assert.IsTrue(chisquare1 == 26.133333333333333d);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 2);
            dict.Add("ghi", 1);
            var sentences = new string[] { "abc def ghi jkl", "abc def ghi", "abc def", "abc abc", "abc"};
            clusterer.Sentences = sentences;
            clusterer.WordCount = dict;
            clusterer.InitializeFrequentTerms(dict);
            clusterer.Initialize();
            var mutualInformation = clusterer.GetMutualInformation("abc","def"); // there's only one
            Assert.IsTrue(mutualInformation == 0.0d);
        }
    }
}
