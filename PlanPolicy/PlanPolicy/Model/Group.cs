using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanPolicy.Model
{
    public class Group
    {
        public string Name { get; set; }
        public Plan<Metrics> Plan { get; set; }
    }
}
