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
            clusterer.Initialize(dict, sentences);
            var matrix = clusterer.CooccurenceMatrix;
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
            clusterer.Initialize(dict, sentences);
            var chisquare =  clusterer.GetChiSquare("abc");
            Assert.IsTrue(chisquare == 0);
            var chisquare1 = clusterer.GetChiSquare("def");
            Assert.IsTrue(chisquare1 == 6.4d);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 6);
            dict.Add("def", 3);
            dict.Add("ghi", 2);
            dict.Add("jkl", 1);
            var sentences = new string[] { "abc def ghi jkl", "abc def ghi", "abc def", "abc abc", "abc"};
            clusterer.Initialize(dict, sentences);
            var mutualInformation = clusterer.GetMutualInformation("abc", "def"); // there's only one
            Assert.IsTrue(mutualInformation == 0.0d);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 6);
            dict.Add("def", 3);
            dict.Add("ghi", 2);
            dict.Add("jkl", 1);
            var sentences = new string[] { "abc def ghi jkl", "abc def ghi", "abc def", "abc abc", "abc" };
            clusterer.Initialize(dict, sentences);
            clusterer.Classify();
            Assert.IsTrue(clusterer.Clusters.Count == 1);
            Assert.IsTrue(clusterer.Clusters[0].Members.Count == 4);
        }
    }
}
