using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatsuoKeywordExtractor
{
    public interface IDistance
    {
        double GetDistance(Word di, Word dj);
    }
}
