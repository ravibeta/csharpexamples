using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeries;

namespace TimeSeriesTest
{
    [TestClass]
    public class TimeSeriesTest
    {
        private TimeSeries<int> timeSeries;
        private List<int> input;

        [TestInitialize]
        public void Setup()
        {
            timeSeries = new TimeSeries<int>();
            input = new List<int>() { 35, 36, 39, 64, 88, 89, 99, 100 };
        }

        [TestMethod]
        public void TestWindowing()
        {
            var ret = timeSeries.GetWindowingTransformations(input, 3);
            Assert.IsTrue(ret.Count == 6);
            var first = ret.First();
            Assert.IsTrue(first.Count == 3);
            Assert.IsTrue(first.First() == 35 && first.ElementAt(1) == 36 && first.Last() == 39);
            var last = ret.Last();
            Assert.IsTrue(last.Count == 3);
            Assert.IsTrue(last.First() == 89 && last.ElementAt(1) == 99 && last.Last() == 100);
        }

        [TestMethod]
        public void TestRandomVariable()
        {
            var ret = timeSeries.GetRandomVariablePlotCentered(input);
            Assert.IsTrue(ret.Any(x => x < 0));
            Assert.IsTrue(ret.Count == input.Count);
            Assert.IsTrue(ret.Average() == 0);
        }

        [TestMethod]
        public void TestLinearRegressionFit()
        {
            // var rv = timeSeries.GetRandomVariablePlotCentered(input);
            var ret = timeSeries.GetRegression(input);
            Assert.IsTrue(ret.First().Key == 35);
            Assert.IsTrue(ret.First().Value == 8);
        }

        [TestMethod]
        public void TestARTree()
        {
            var dict = timeSeries.GetARTree(input);
            double lastBayesianScore = 0;
            foreach (var kvp in dict)
            {
                var temp = new Dictionary<double, double>();
                temp.Add(kvp.Key, kvp.Value);
                var score  = timeSeries.GetBayesianScore(input, temp);
                Assert.IsTrue(score > lastBayesianScore);
                lastBayesianScore = score;
            }
        }
    }
}
