using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByKLD
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Count() == 0) return;
                var fileContent = File.ReadAllText(args[0]);
                var sentences = fileContent.Split(new char[] { '?', '.', '!' });
                var words = fileContent.Split(new char[] { '?', '!', '.', ',', '/', ';', ':', '<', '>', '{', '}', '[', ']', '(', ')', '-', '\'', '\\', '`', '@', '#', '$', '%', '^', '&', '+', '*', '=' });
                // Step1 : Preprocessing and stemming
                // Step2 : Selection of frequent terms
                // Step3 : Clustering frequent terms
                // Step4 : Calculation of expected probability
                // Step5 : Calculation of chi-square value
                // Step6 : Output Keywords
                var dict = new SortedDictionary<string, int>();
                var stopWords = File.ReadAllText(@"StopWords.txt").Split(new char[] { ',' });
                foreach (var word in words)
                {
                    if (stopWords.Contains(word) == false)
                    {
                        if (dict.ContainsKey(word))
                            dict[word] += 1;
                        else
                            dict.Add(word, 1);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
