using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var itemBuyerAge = new Dictionary<string, int>();
            itemBuyerAge.Add("ABC", 32);
            itemBuyerAge.Add("DEF", 59);
            itemBuyerAge.Add("GHI", 64);
            var ret = GetComputedColumn(itemBuyerAge);
            if (ret != null)
            {
                foreach (var kvp in ret)
                {
                    Console.WriteLine("Buyer = {0}, Age = {1}", kvp.Key, kvp.Value == 1 ? "High" : "Low");
                }
            }
        }

        public static Dictionary<string, int> GetComputedColumn(Dictionary<string, int> itemBuyerAge)
        {
            // TODO: use Shannon's measure
            // H(X) = -∑ P(xi) log(P(xi))
            int count = 0;
            foreach (var kvp in itemBuyerAge)
            {
                if (kvp.Value > 50) count++;
            }
            if (count > (double) 0.6 * itemBuyerAge.Values.Count)
            {
                var dict  = new Dictionary<string,int>();
                foreach (var kvp in itemBuyerAge)
                {
                    dict.Add(kvp.Key, kvp.Value > 50 ?  1 : 0);
                }
                return dict;
            }
            return null;
        }
    }
}
