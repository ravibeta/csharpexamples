using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimb
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] v = new int[] { 5, 4, 1, 2, 3, 8 };
            hill(v);
        }
        static public void hill(int[] v)
        {
            if (v == null || v.Length == 0) return;
            int max = 0;
            int min = 0;
            int diff = max - min;
            for (int i = 1; i < v.Length; i++)
            {
                if (v[i] < v[i - 1])
                {
                    if (v[i - 1] > max) { max = v[i - 1]; min = v[i];  }
                    if (v[i] < min) { min = v[i]; }
                }
                else
                {
                    if (max - min > diff)
                        diff = max - min;
                }
            }
            Console.WriteLine("{0}", diff);
        }
    }
}
