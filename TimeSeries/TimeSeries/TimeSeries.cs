//-----------------------------------------------------------------------
// <copyright file="TimeSeries.cs" company="RaviRajamani">
//     Copyright (c) Ravi Rajamani. All rights reserved.
// </copyright>
// <author>Ravi Rajamani</author>
//-----------------------------------------------------------------------

namespace TimeSeries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Encapsulates the steps for the Microsoft TimeSeries Algorithm.
    /// </summary>
    /// <typeparam name="T"> T can be any time-series event type that can be converted to a decimal on the scatter plot</typeparam>
    public class TimeSeries<T>
    {
        /// <summary>
        /// Gets or sets the p-length for transforming the Time Series events.
        /// </summary>
        private int PLength { get; set; }

        /// <summary>
        /// This returns the p-length transformations from the time series data.
        /// </summary>
        /// <param name="timeSeriesData">list of time series events</param>
        /// <param name="p">p-length for the transformation</param>
        /// <returns>the list of windowing transformations</returns>
        public List<List<T>> GetWindowingTransformations(List<T> timeSeriesData, int p)
        {
            this.PLength = p;
            return null;
        }

        /// <summary>
        /// This centers and standardizes the random variable plot.
        /// </summary>
        /// <param name="timeSeriesData">list of time series events</param>
        /// <returns>the list of the centered and standardized random variable</returns>
        public List<double> GetRandomVariablePlotCentered(List<T> timeSeriesData)
        {
            List<List<T>> transformedData = this.GetWindowingTransformations(timeSeriesData, this.PLength);
            if (transformedData == null || transformedData.Count == 0)
            {
                throw new InvalidOperationException();
            }

            Func<T, decimal?> converter = c => Convert.ToDecimal(c);
            List<double> ret = new List<double>();
            foreach (var xi in transformedData)
            {
                decimal? mean = xi.Average<T>(converter);
                decimal? min = xi.Min<T>(converter);
                decimal? max = xi.Max<T>(converter);
                var diff = xi.Select(x => Math.Pow((double)(Convert.ToDecimal(x) - mean), 2));
                double sd = Math.Sqrt(((double)diff.Sum()) / ((double)diff.Count()));
                double zmin = ((double)(mean - min)) / sd;
                double zmax = ((double)(max - mean)) / sd;
                //// var centeredAndStandardized = xi.Select(x => (Convert.ToDouble(x) - Convert.ToDouble(mean.Value)) / sd).ToList();
                ret.Add((zmin + zmax) / 2);
            }

            return ret;
        }

        /// <summary>
        /// This fits a line to the scatter plot.
        /// </summary>
        /// <param name="rv">list of random variable plot data</param>
        /// <returns>the slope and constant for the best linear fit to the plot data</returns>
        public Dictionary<double, double> GetRegression(List<T> rv)
        {
            double origin = 0;
            double slope = 0;
            var dict = new Dictionary<double, double>();
            dict.Add(origin, slope);
            throw new NotImplementedException();
            //// return dict;
        }

        /// <summary>
        /// This gives the Bayesian Score for the fit.
        /// </summary>
        /// <param name="input">list of centered and standardized random variable data</param>
        /// <param name="regression">The AR Tree (piece-wise linear fit) as defined in the documentation</param>
        /// <returns>the Bayesian score as a double</returns>
        public double GetBayesianScore(List<T> input, Dictionary<double, double> regression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This returns the slope and constant for each piecewise linear fit in the AR Tree.
        /// </summary>
        /// <param name="rv"> the list of centered and standardized random variable data</param>
        /// <returns>returns the slope and constant for each piecewise linear fit in the AR Tree</returns>
        public Dictionary<double, double> GetARTree(List<T> rv)
        {
            // split leaf recursively
            return null;
        }
    }
}
