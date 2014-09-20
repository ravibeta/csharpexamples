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

    public class TimeSeries<T>
    {
        private int PLength { get; set; }
        public List<List<T>> GetWindowingTransformations(List<T> timeSeriesData, int p) // for p-length transformations
        {
            PLength = p;
            return null;
        }

        public List<Double> GetRandomVariablePlotCentered(List<T> timeSeriesData)
        {
            List<List<T>> transformedData = GetWindowingTransformations(timeSeriesData, PLength);
            if (transformedData == null || transformedData.Count == 0) throw new InvalidOperationException();
            Func<T, decimal?> converter = c => Convert.ToDecimal(c);
            List<Double> ret = new List<Double>();
            foreach (var xi in transformedData)
            {
                decimal? mean = xi.Average<T>(converter);
                decimal? min = xi.Min<T>(converter);
                decimal? max = xi.Max<T>(converter);
                var diff = xi.Select(x => Math.Pow((double)(Convert.ToDecimal(x) - mean), 2));
                double sd = Math.Sqrt(diff.Sum() / diff.Count());
                double zmin = (double)(mean - min) / sd;
                double zmax = (double)(max - mean)/ sd;
                // var centeredAndStandardized = xi.Select(x => (Convert.ToDouble(x) - Convert.ToDouble(mean.Value)) / sd).ToList();
                ret.Add((zmin + zmax)/2);
            }
            return ret;
        }

        public Dictionary<double, double> GetRegression(List<T> rv)
        {
            double origin = 0;
            double slope = 0;
            var dict = new Dictionary<double, double>();
            dict.Add(origin, slope);
            throw new NotImplementedException();
            return dict;
        }

        public double GetBayesianScore(List<T> input, Dictionary<double, double> regression)
        {
            throw new NotImplementedException();
        }

        public Dictionary<double, double> GetARTree(List<T> rv)
        {
            // split leaf recursively
            return null;
        }
    }
}
