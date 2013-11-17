using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterer
{
    public interface IDistance
    {
        double GetDistance(Document di, Document dj);
    }
}
