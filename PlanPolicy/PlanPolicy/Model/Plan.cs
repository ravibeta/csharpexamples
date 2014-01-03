using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanPolicy.Model
{
    public class Plan<T> : IPlan where T : Metrics
    {
        public static Plan<T> Create(MetricType type, double slice) { throw new NotImplementedException(); }
        public bool ReClassify() { throw new NotImplementedException(); }
        public bool IsValid() { throw new NotImplementedException(); }
    }
}
