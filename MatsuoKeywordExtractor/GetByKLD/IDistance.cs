using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByKLD
{
    public interface IDistance
    {
        double GetDistance(Word di, Word dj);
    }
}
