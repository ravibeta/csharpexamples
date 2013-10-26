using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterer.Model
{
    public class Doggie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Label Label { get; set; }
    }

    public enum Label
    {
        Beagles, // high height high weight
        Chihuahuas, // low height low weight
        Daschunds, // low height high weight
    }
}
