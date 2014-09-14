using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries
{
    public class TimeSeries<T>
    {
        public List<List<T>> GetWindowingTransformations(List<T> timeSeriesData, int p) // for p-length transformations
        {
            return null;
        }

        public List<Double> GetRandomVariablePlotCentered(List<T> timeSeriesData)
        {
            if (timeSeriesData == null || timeSeriesData.Count == 0) throw new InvalidOperationException();
            Func<T, decimal?> converter = c => Convert.ToDecimal(c);
            decimal? mean = timeSeriesData.Average<T>(converter);
            var diff = timeSeriesData.Select(x => Math.Pow((double) (Convert.ToDecimal(x) - mean),  2));
            double sd = Math.Sqrt(diff.Sum() / diff.Count());
            return timeSeriesData.Select(x => (Convert.ToDouble(x) - Convert.ToDouble(mean.Value)) / sd).ToList();
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
