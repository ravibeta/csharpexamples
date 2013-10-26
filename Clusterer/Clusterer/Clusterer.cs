using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clusterer.Model;

namespace Clusterer
{
    public class Clusterer
    {
        public List<Doggie> Doggies { get; set; }
        public List<Doggie> Classify() 
        {
            if (Doggies != null && Doggies.Count() < 3) return Doggies;

            // initial assignment- Three clusters seeds
            var chihuahuaCentroid = Doggies.Min(x => x.Weight);
            var beaglesCentroid = Doggies.Max(x => x.Height);
            var DaschundCentroid = Doggies.ElementAt(Doggies.Count / 2);


            return Doggies;
        }
    }
}
