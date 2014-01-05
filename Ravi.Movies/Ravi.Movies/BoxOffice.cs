using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ravi.Movies.Model;
using Ravi.Movies.Extensions;

namespace Ravi.Movies
{
    public class BoxOffice
    {
        public bool IsMatinee { get; set; }
        public bool IsGroup { get; set; }
        public bool IsThreeD { get; set; }

        public decimal Calculate(List<int> ages)
        {
            var price = 0.0m;
            var grp = ages.ToGroup();
            grp.priceJuniors = 5;
            grp.priceAdults = 10;
            grp.priceSeniors = 8;

            if (IsMatinee)
            {
                grp.priceAdults = 7;
                grp.priceSeniors = 7;
            }

            if (IsGroup)
            {
                grp.priceAdults = 6;
                grp.priceSeniors = 6;
            }

            if (IsThreeD)
            {
                grp.priceJuniors += 3;
                grp.priceAdults += 3;
                grp.priceSeniors += 3;
            }

            return price;
        }
    }
}
