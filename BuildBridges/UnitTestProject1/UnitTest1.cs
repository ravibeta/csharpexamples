using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ConsoleApplication2;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var thisShore = new List<Char>() { 'a' , 'b', 'c', 'd', 'e' , 'f'};
            var thatShore = new List<Char>() { 'f', 'b', 'e', 'd', 'x', 'a' };
            var bridges = Program.GetBridges(thisShore, thatShore);
            Assert.IsTrue(bridges.Count == 2);
            Assert.IsTrue(bridges[0] == 'b');
            Assert.IsTrue(bridges[1] == 'd');
        }
    }
}
