//-----------------------------------------------------------------------
// <copyright file="TimeSeriesTest.cs" company="RaviRajamani">
//     Copyright (c) Ravi Rajamani. All rights reserved.
// </copyright>
// <author>Ravi Rajamani</author>
//-----------------------------------------------------------------------

namespace TimeSeriesTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TimeSeries;

    /// <summary>
    /// Unit Tests for Microsoft Time Series Implementation Method.
    /// </summary>
    [TestClass]
    public class TimeSeriesTest
    {
        /// <summary>
        /// The target for the test.
        /// </summary>
        private TimeSeries<int> timeSeries;

        /// <summary>
        /// The time series data for the test.
        /// </summary>
        private List<int> input;

        /// <summary>
        /// Initialization of the tests
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.timeSeries = new TimeSeries<int>();
            this.input = new List<int>() { 35, 36, 39, 64, 88, 89, 99, 100 };
        }

        /// <summary>
        /// test windowing transformation of time series data
        /// </summary>
        [TestMethod]
        public void TestWindowing()
        {
            var ret = this.timeSeries.GetWindowingTransformations(this.input, 3);
            Assert.IsTrue(ret.Count == 6);
            var first = ret.First();
            Assert.IsTrue(first.Count == 3);
            Assert.IsTrue(first.First() == 35 && first.ElementAt(1) == 36 && first.Last() == 39);
            var last = ret.Last();
            Assert.IsTrue(last.Count == 3);
            Assert.IsTrue(last.First() == 89 && last.ElementAt(1) == 99 && last.Last() == 100);
        }

        /// <summary>
        /// test centering and standardizing random variable for fitting a linear AR model
        /// </summary>
        [TestMethod]
        public void TestRandomVariable()
        {
            var ret = this.timeSeries.GetRandomVariablePlotCentered(this.input);
            Assert.IsTrue(ret.Any(x => x < 0));
            Assert.IsTrue(ret.Count == this.input.Count);
            Assert.IsTrue(ret.Average() == 0);
        }

        /// <summary>
        /// test the fit of a linear AR model
        /// </summary>
        [TestMethod]
        public void TestLinearRegressionFit()
        {
            // var rv = this.timeSeries.GetRandomVariablePlotCentered(input);
            var ret = this.timeSeries.GetRegression(this.input);
            Assert.IsTrue(ret.First().Key == 35);
            Assert.IsTrue(ret.First().Value == 8);
        }

        /// <summary>
        /// This fits a line to the scatter plot.
        /// </summary>
        [TestMethod]
        public void TestARTree()
        {
            var dict = this.timeSeries.GetARTree(this.input);
            double lastBayesianScore = 0;
            foreach (var kvp in dict)
            {
                var temp = new Dictionary<double, double>();
                temp.Add(kvp.Key, kvp.Value);
                var score = this.timeSeries.GetBayesianScore(this.input, temp);
                Assert.IsTrue(score > lastBayesianScore);
                lastBayesianScore = score;
            }
        }
    }
}
