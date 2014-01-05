using Ravi.Movies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravi.Movies.Extensions
{
    public static class BoxOfficeExtensions
    {
        public static bool IsGroup(this List<int> ages)
        {
            return ages.Count > 6;
        }

        public static Group ToGroup(this List<int> ages)
        {
            var group = new Group();
            foreach (var age in ages)
            {
                if (age < 13) group.Juniors++;
                if (age < 64) group.Adults++;
                else
                    group.Seniors++;
            }
            return group;
        }
    }
}
