using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravi.Movies.Model
{
    public class Group
    {
        public int Juniors { get; set; }
        public int Adults { get; set; }
        public int Seniors { get; set; }
        public int priceJuniors { get; set; }
        public int priceAdults { get; set; }
        public int priceSeniors { get; set; }

        public Group()
        {
            priceJuniors = 5;
            priceAdults = 10;
            priceSeniors = 8;
            Juniors = 0;
            Adults = 0;
            Seniors = 0;
        }

        public decimal Calculate()
        {
            return Juniors * (decimal)priceJuniors + Adults * (decimal)priceAdults + Seniors * (decimal)priceSeniors;
        }
    }
}
