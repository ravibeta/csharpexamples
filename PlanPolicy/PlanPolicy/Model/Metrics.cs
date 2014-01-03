using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanPolicy.Model
{
    public class Metrics
    {
        public MetricType Type { get; set; }
        public Double Measure { get; set; }
        public string Unit { get; set; }
    }

    public enum MetricType
    {
        CPU = 0,
        Memory = 1
    }
}
