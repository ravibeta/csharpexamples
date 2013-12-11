using IndexSum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() { 1, 1, 2, 2, 4 };
            PrintSumCombinations(numbers, 4);
        }

        public static void PrintSumCombinations(List<int> numbers, int n)
        {
            if (numbers.Count == 0) return;
            numbers.Sort();
            if (numbers.ElementAt(0) > n) return;

            // initialize
            var indexedNumbers = numbers.ToIndexedNumbers();
            var candidate = new List<IndexedNumber>();
            var sequences = new List<List<IndexedNumber>>();
            
            //Act
            PermuteAndFind(ref indexedNumbers, ref candidate, ref sequences, n);

            // Print
            Console.WriteLine(sequences.ToString(true));

        }

        public static void PermuteAndFind(ref List<IndexedNumber> numbers, ref List<IndexedNumber> candidate, ref List<List<IndexedNumber>> sequences, int n)
        {
            if (candidate.Sum() == n)
            {
                sequences.Add(new List<IndexedNumber>(candidate));
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i].Used) continue;
                candidate.Add(numbers[i]);
                numbers[i].Used = true;
                PermuteAndFind(ref numbers, ref candidate, ref sequences, n);
                candidate.Remove(numbers[i]);
                numbers[i].Used = false;
            }
        }





    }
}
