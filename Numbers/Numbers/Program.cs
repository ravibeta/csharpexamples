using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string numbers = "1,2,3,4,5,6,7,8,9";
            var num = ToNumbers(numbers, 5);
            Console.WriteLine("Sum = {0}", ToNumbers(numbers, -1).Sum());
            List<int> numerals = Reverse(numbers);
            Console.WriteLine(ReverseString(numbers));
        }

        public static List<int> ToNumbers(string commaSeparatedNumbers, int start)
        {
            if (commaSeparatedNumbers == null) return null;
            var candidates = commaSeparatedNumbers.Split(new char[] { ',' }).ToList();
            candidates.RemoveRange(0, start - 1 > 0 && start < candidates.Count() ? start - 1 : 0);
            Converter<string, Int32> converter = s => { Int32 result; return Int32.TryParse(s, out result) ? result : 0; };
            return candidates.ConvertAll<Int32>(converter).ToList();
        }

        public static string ToString(List<int> numbers, int start)
        {
            string result = String.Empty;
            numbers.ForEach(x => result += x.ToString() + ", ");
            result = result.TrimEnd(new char[] {',', ' '});
            return result;
        }

        public static List<int> Reverse(string commaSeparated)
        {
            var numbers = ToNumbers(commaSeparated, 0);
            numbers.Reverse();
            Console.WriteLine(ToString(numbers, 0));
            return numbers;
        }

        public static string ReverseString(string commaSeparated)
        {
            var words = commaSeparated.Split(new char[] { ',', ' ' });
            var reversed = words.Aggregate((sent, next) => next + ", " + sent);
            return reversed;
        }
    }
}
