using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdRide.Models;

namespace NerdRide.Models
{
    public class FlairViewModel
    {
        public IList<Ride> Rides { get; set; }
        public string LocationName { get; set; }
    }
}
