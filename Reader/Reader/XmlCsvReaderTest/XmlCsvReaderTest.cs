using XmlCsvReaderNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;

namespace XmlCsvReaderTest
{
    
    
    /// <summary>
    ///This is a test class for XmlCsvReaderTest and is intended
    ///to contain all XmlCsvReaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlCsvReaderTest
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
        ///A test for XmlCsvReader Constructor
        ///</summary>
        [TestMethod()]
        public void XmlCsvReaderConstructorTest()
        {
            XmlCsvReader target = new XmlCsvReader();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Close
        ///</summary>
        [TestMethod()]
        public void CloseTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            target.Close();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetAttribute
        ///</summary>
        [TestMethod()]
        public void GetAttributeTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetAttribute(i);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttribute
        ///</summary>
        [TestMethod()]
        public void GetAttributeTest1()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string namespaceURI = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetAttribute(name, namespaceURI);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttribute
        ///</summary>
        [TestMethod()]
        public void GetAttributeTest2()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetAttribute(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LookupNamespace
        ///</summary>
        [TestMethod()]
        public void LookupNamespaceTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string prefix = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.LookupNamespace(prefix);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveToAttribute
        ///</summary>
        [TestMethod()]
        public void MoveToAttributeTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string ns = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveToAttribute(name, ns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveToAttribute
        ///</summary>
        [TestMethod()]
        public void MoveToAttributeTest1()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveToAttribute(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveToElement
        ///</summary>
        [TestMethod()]
        public void MoveToElementTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveToElement();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveToFirstAttribute
        ///</summary>
        [TestMethod()]
        public void MoveToFirstAttributeTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveToFirstAttribute();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveToNextAttribute
        ///</summary>
        [TestMethod()]
        public void MoveToNextAttributeTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveToNextAttribute();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Read
        ///</summary>
        [TestMethod()]
        public void ReadTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Read();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReadAttributeValue
        ///</summary>
        [TestMethod()]
        public void ReadAttributeValueTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ReadAttributeValue();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ResolveEntity
        ///</summary>
        [TestMethod()]
        public void ResolveEntityTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            target.ResolveEntity();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AttributeCount
        ///</summary>
        [TestMethod()]
        public void AttributeCountTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.AttributeCount;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BaseURI
        ///</summary>
        [TestMethod()]
        public void BaseURITest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.BaseURI;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Depth
        ///</summary>
        [TestMethod()]
        public void DepthTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.Depth;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EOF
        ///</summary>
        [TestMethod()]
        public void EOFTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.EOF;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsEmptyElement
        ///</summary>
        [TestMethod()]
        public void IsEmptyElementTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsEmptyElement;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LocalName
        ///</summary>
        [TestMethod()]
        public void LocalNameTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.LocalName;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NameTable
        ///</summary>
        [TestMethod()]
        public void NameTableTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            XmlNameTable actual;
            actual = target.NameTable;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NamespaceURI
        ///</summary>
        [TestMethod()]
        public void NamespaceURITest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.NamespaceURI;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NodeType
        ///</summary>
        [TestMethod()]
        public void NodeTypeTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            XmlNodeType actual;
            actual = target.NodeType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Prefix
        ///</summary>
        [TestMethod()]
        public void PrefixTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Prefix;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReadState
        ///</summary>
        [TestMethod()]
        public void ReadStateTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            ReadState actual;
            actual = target.ReadState;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            XmlCsvReader target = new XmlCsvReader(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Value;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod]
        public void TestXmlReader()
        {
            XmlDocument doc = new XmlDocument();
            XmlCsvReader reader = new XmlCsvReader(new Uri("file:///C:/temp/input.csv"), doc.NameTable);
            reader.FirstRowHasColumnNames = true;
            reader.RootName = "customers";
            reader.RowName = "customer";

            doc.Load(reader);
            Console.WriteLine(doc.OuterXml);
            doc.Save("output.xml"); 
            
        }
    }
}
