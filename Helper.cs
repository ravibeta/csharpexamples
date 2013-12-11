using IndexSum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexSum
{
    public static class Helper
    {
        public static List<IndexedNumber> ToIndexedNumbers(this List<int> numbers)
        {
            var indexedNumbers = numbers.Select((x, i) => new IndexedNumber() { Number = numbers.ElementAt(i), Index = i, Used = false }).ToList();
            return indexedNumbers;
        }

        public static int Sum(this List<IndexedNumber> indexedNumbers)
        {
            return indexedNumbers.Select(x => x.Number).Sum();
        }

        public static string ToString(this List<List<IndexedNumber>> sequences, bool removeDuplicates = true)
        {
            string output = string.Empty;
            sequences.ToList().ForEach(x =>
            {
                string sequence = string.Empty;
                x.ForEach(t => sequence += t.Index.ToString() + " ");
                sequence += "\r\n";
                if (removeDuplicates)
                {
                    if (output.Contains(sequence) == false)
                    {
                        // eliminate duplicates
                        output += sequence;
                    }
                }
                else
                {
                    output += sequence;
                }
            });
            return output;
        }
    }
}
