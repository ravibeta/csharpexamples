using Allocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Allocator;

namespace AllocatorTest
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AllocatorTest
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
        public void TestLarge()
        {
            var alc = new Allocator.Allocator();
            int index = alc.Alloc(10);
            Assert.IsTrue(index >= 20);
            alc.Free(index);
            Assert.IsTrue(alc.FreeListLarge.Count == 1);
        }
        [TestMethod()]
        public void TestAllocFreeSmall()
        {
            var alc = new Allocator.Allocator();
            int index = alc.Alloc(4);
            alc.Free(index);
            Assert.IsTrue(alc.FreeListSmall.Count == 1);
        }

        [TestMethod()]
        public void TestAllocAllocFreeFreeAllocSmall()
        {
            var alc = new Allocator.Allocator();
            int first = alc.Alloc(4);
            int second = alc.Alloc(3);
            alc.Free(second);
            Assert.IsTrue(alc.FreeListSmall.Count == 1);
            alc.Free(first);
            Assert.IsTrue(alc.FreeListSmall.Count == 2);
            int third = alc.Alloc(2);
            Assert.IsTrue(second == third);
        }
    }
}
