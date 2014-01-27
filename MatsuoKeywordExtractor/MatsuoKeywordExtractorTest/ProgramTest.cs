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
            var chisquare = clusterer.GetChiSquare("abc");
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
            var sentences = new string[] { "abc def ghi jkl", "abc def ghi", "abc def", "abc abc", "abc" };
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
            clusterer.ThresholdFactor = 1.0d;
            clusterer.Initialize(dict, sentences);
            clusterer.Classify();
            Assert.IsTrue(clusterer.Clusters.Count == 1);
            Assert.IsTrue(clusterer.Clusters[0].Members.Count == 4);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 6);
            dict.Add("def", 3);
            dict.Add("ghi", 2);
            dict.Add("jkl", 1);
            var sentences = new string[] { "abc def ghi jkl", "abc def ghi", "abc def", "abc abc", "abc" };
            clusterer.ThresholdFactor = 0.3d;
            clusterer.Initialize(dict, sentences);
            clusterer.Classify();
            Assert.IsTrue(clusterer.Clusters.Count == 1);
            Assert.IsTrue(clusterer.Clusters[0].Members.Count == 1);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 3);
            dict.Add("ghi", 4);
            dict.Add("jkl", 2);
            var sentences = new string[] { "abc def jkl", "ghi jkl", "abc def", "abc def", "ghi ghi", "ghi" };
            clusterer.ThresholdFactor = 0.7d;
            clusterer.Initialize(dict, sentences);
            clusterer.Classify();
            Assert.IsTrue(clusterer.Clusters.Count == 2);
            Assert.IsTrue(clusterer.Clusters.Any(x => x.Members.Count == 2));
            Assert.IsTrue(clusterer.Clusters.Any(x => x.Members.Count == 1));
        }

        [TestMethod]
        public void TestKLDFilter()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 3);
            dict.Add("ghi", 4);
            dict.Add("jkl", 2);
            dict.Add("mno", 1);
            var sentences = new string[] { "abc def jkl mno", "ghi jkl", "abc def", "abc def", "ghi ghi", "ghi" };
            clusterer.ThresholdFactor = 0.7d;
            clusterer.Initialize(dict, sentences);
            var ret = clusterer.GetKeywordsBasedOnKLD();
            Assert.IsTrue(ret.Count > 0);
        }

        [TestMethod]
        public void TestKLDClassifier()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("abc", 3);
            dict.Add("def", 3);
            dict.Add("ghi", 4);
            dict.Add("jkl", 2);
            var sentences = new string[] { "abc def jkl", "ghi jkl", "abc def", "abc def", "ghi ghi", "ghi" };
            clusterer.ThresholdFactor = 0.7d;
            clusterer.Initialize(dict, sentences);
            clusterer.CoOccurrenceClassify();
            Assert.IsTrue(clusterer.Clusters.Count == 3);
        }
        [TestMethod]
        public void TestKLDClassifierWithNewSample()
        {
            var sentences = new string[] { "Clustering and Segmentation", "Clustering is a data mining technique that is directed towards the goals of identification and classification", "Clustering tries to identify a finite set of categories or clusters to which each data object (tuple) can be mapped", "The categories may be disjoint or overlapping and may sometimes be organized into trees", "For example, one might form categories of customers into the form of a tree and then map each customer to one or more of the categories", "A closely related problem is that of estimating multivariate probability density functions of all variables that could be attributes in a relation or from different relations" };
            var dict = new Dictionary<string, int>();
            dict.Add("categories", 4);
            dict.Add("clustering", 3);
            dict.Add("data", 2);
            dict.Add("attribute", 1);
            clusterer.ThresholdFactor = 0.7d;
            clusterer.Initialize(dict, sentences);
            clusterer.CoOccurrenceClassify();
            Assert.IsTrue(clusterer.Clusters.Count == 3);
            Assert.IsTrue(clusterer.Labels.Count == 4);
            Assert.IsTrue(clusterer.Labels.Find(x => x.Term == "clustering").ClusterIndex == clusterer.Labels.Find(x => x.Term == "categories").ClusterIndex);
            Assert.IsTrue(clusterer.Labels.GroupBy(x => x.ClusterIndex).Any(x => x.Count() >= 2));
            clusterer.KMeans = 2;
            clusterer.ThresholdFactor = 0.7d;
            clusterer.Initialize(dict, sentences);
            clusterer.CoOccurrenceClassify();
            Assert.IsTrue(clusterer.Clusters.Count == 2);
            Assert.IsTrue(clusterer.Labels.Count == 4);
            Assert.IsTrue(clusterer.Labels.Find(x => x.Term == "clustering").ClusterIndex == clusterer.Labels.Find(x => x.Term == "categories").ClusterIndex);
            // Assert.IsTrue(clusterer.Labels.Count(x => x.ClusterIndex == 0) == 3);
            // Assert.IsTrue(clusterer.Labels.Count(x => x.ClusterIndex == 1) == 1);

        }
    }
}
