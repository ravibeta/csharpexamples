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
    }
}
