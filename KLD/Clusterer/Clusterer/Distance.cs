using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterer
{
    public class Distance : IDistance
    {
        public double GetDistance(Document di, Document dj)
        {
            Debug.Assert(di.Vocabulary.Count() == dj.Vocabulary.Count());

            double total = 0;
            foreach (var term in di.Vocabulary)
            {
                var dik = di.GetProbability(term);
                var djk = dj.GetProbability(term);
                total += (dik - djk) * Math.Log(dik / djk);
            }
            return total;
        }

        public double GetNormalizedDistance(Document di, Document dj)
        {
            var emptyDocument = Document.GetEmptyDocument(di.Vocabulary);
            var KLDij = GetDistance(di, dj);
            var KLDi0 = GetDistance(di, emptyDocument);
            if (KLDi0 > 0)
            {
                return KLDij / KLDi0;
            }
            return KLDij;
        }
    }
}
