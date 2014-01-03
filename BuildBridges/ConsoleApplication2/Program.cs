using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    // Cities are labeled by alphabets. 
    // They appear on either or both sides of a river.
    // They are all distinct.
    // Build bridges between same name cities

    public static class Program
    {
        static void Main(string[] args)
        {
        }
        public static List<Char> GetBridges(List<Char> thisShore, List<Char> thatShore)
        {
            var commonOrdered = new List<Char>();
            for (int i = 0; i < thisShore.Count; i++)
                for (int j = 0; j < thatShore.Count; j++)
                {
                    if (thisShore[i] == thatShore[j] &&
                        commonOrdered.Contains(thisShore[i]) == false)
                    {
                        commonOrdered.Add(thisShore[i]);
                    }
                }
            // remove out of order elements from first
            var filtered = RemoveOutOfOrder(thisShore, commonOrdered);
            // remove out of order elements from second
            var secondfiltered = RemoveOutOfOrder(thatShore, filtered);
 
            return secondfiltered;
        }

        private static List<Char> RemoveOutOfOrder(List<Char> source, List<Char> commonOrdered)
        {
            int difference = 0;
            // remove out of order elements from first
            var filtered = new List<Char>();
            for (int i = 1; i < commonOrdered.Count; i++)
            {
                var index = source.IndexOf(commonOrdered[i]);
                var prevIndex = source.IndexOf(commonOrdered[i - 1]);
                if (index > prevIndex)
                {
                    difference = index - prevIndex;
                    if (filtered.Contains(commonOrdered[i - 1]) == false) filtered.Add(commonOrdered[i - 1]);
                    if (filtered.Contains(commonOrdered[i]) == false) filtered.Add(commonOrdered[i]);
                }
            }
            return filtered;
        }
    }
}
