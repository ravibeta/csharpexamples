using BookIndexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SkmDataStructures2;
using System.Collections.Generic;

namespace BookIndexingTest
{
    
    
    /// <summary>
    ///This is a test class for Program_SalientWordsTest and is intended
    ///to contain all Program_SalientWordsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Program_SalientWordsTest
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
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            SkipList<WordInfo> skipList = new SkipList<WordInfo>();
            skipList.Add(new WordInfo() { Canon = "clustering", Word = "Clustering", Frequency = 3, Offset = new long[] { 11 }, Page = new int[] { 0 }});
            skipList.Add(new WordInfo() { Canon = "categories", Word = "categories", Frequency = 2, Offset = new long[] { 23 }, Page = new int[] { 0 }});
            Program.SalientWords target = new Program.SalientWords(skipList); // TODO: Initialize to an appropriate value
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("clustering", "1" );
            Dictionary<string, string> actual;
            actual = target.Index();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
