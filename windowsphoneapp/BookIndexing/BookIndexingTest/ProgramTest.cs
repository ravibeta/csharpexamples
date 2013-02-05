using BookIndexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BookIndexingTest
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProgramTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Main
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BookIndexing.exe")]
        public void MainTest()
        {
            string[] args = null; // TODO: Initialize to an appropriate value
            Program_Accessor.Main(args);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string text = "Clustering and Segmentation. Clustering is a data mining technique that is directed towards the goals of identification and classification. Clustering tries to identify a finite set of categories or clusters to which each data object (tuple) can be mapped. The categories may be disjoint or overlapping and may sometimes be organized into trees. For example, one might form categories of customers into the form of a tree and then map each customer to one or more of the categories. A closely related problem is that of estimating multivariate probability density functions of all variables that could be attributes in a relation or from different relations.";
            var words = text.Split(new char[] { '\t', ' ' });            
            var sdict = new SortedDictionary<string, int>();
            foreach(var word in words)
            {
                if(sdict.ContainsKey(word))
                {
                    sdict[word]++;
                }
                else
                {
                    sdict.Add(word, 1);
                }

            }
            var skipList = Program.Parse(text);
            string sentence1 = string.Empty;
            foreach (var v in skipList)
            {
                sentence1 += (v.Word + " ");
            }
            sentence1 += "\r\n";
            string sentence2 = string.Empty;
            foreach (var v in sdict)
            {
                sentence2 += (v.Key + " ");
            }
            sentence2 += "\r\n";

            int c = 0;
            foreach (var v in sdict)
            {
                c += v.Value;
            }
            //Console.WriteLine("total count of words in dict = {0} and total input count = {1}", c, words.Length);
            //Console.WriteLine("total count of words in skiplist = {0} and count of words in dict = {1}", skipList.Count, sdict.Count);
            //Console.WriteLine();

            Assert.AreEqual(sentence1, sentence2);
            Assert.AreEqual(skipList.Count - 1, sdict.Count);
        }
    }
}
