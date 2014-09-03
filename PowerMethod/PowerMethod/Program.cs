using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] adjacency = { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
            int n = 3;
            
            List<double> b = new List<double>() { 1d, 1d, 1d };

            for (int i = 0; i < 10; i++)
            {
                
                b= GetEigenVectorCentrality(adjacency, n, b, 0.1d).ToList();
            }

            b.ForEach(x => Console.Write(" {0} ", x));
        }

        private static IEnumerable<double> GetEigenVectorCentrality(double[,] adjacency, int N, List<double> b, double tolerance)
        {
            double dd = 1.0d;
            double n = 10d;

            // while (dd > tolerance) 
            {
                List<double> tmp = new List<double>(new double[N]);

                for (int i = 0; i < N; i++)
                {
                    tmp[i] = 0;

                    for (int j = 0; j < N; j++)
                    {
                        tmp[i] += adjacency[i, j] * b[j];
                    }
                }

                dd = Math.Abs(getNorm(b, N) - n);
                var normalized = getNorm(b, N);


                for (int i = 0; i < N; i++)
                {
                    b[i] = tmp[i] / normalized;
                    yield return b[i];
                }
            }
        }

        private static double getNorm(List<double> tmp, int N)
        {
            double normalized_square = 0;

            for (int k = 0; k < N; k++)
            {
                normalized_square += tmp[k] * tmp[k];
            }

            double normalized = Math.Sqrt(normalized_square);

            return normalized;

        }
    }
}
